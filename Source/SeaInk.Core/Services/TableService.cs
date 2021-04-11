using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SeaInk.Core.Entities;
using SeaInk.Core.Entities.Tables;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Styles;
using SeaInk.Core.Models.Tables.Enums;

namespace SeaInk.Core.Services
{
    public class TableService<TTable> where TTable : ITable, new()
    {
        //TODO: Придумать как быть с Division у которого уже есть TableId
        public void CreateTableForDivision(Division division, ISheetMarkup? markup = null)
        {
            markup ??= new DefaultSheetMarkup();

            var table = new TTable();
            division.TableId = table.Create(division.Subject.Title);

            SaveDivision(division, markup);
        }

        public void SaveDivision(Division division, ISheetMarkup? markup = null)
        {
            if (division.TableId == "")
                throw new InvalidDataException();

            markup ??= new DefaultSheetMarkup();

            var table = new TTable();
            table.Load(division.TableId);

            foreach (StudyGroup group in division.Groups)
            {
                table.CreateSheet(new TableIndex(group.Name, division.Groups.IndexOf(group)));
                PasteStudents(division, group, table, markup);
                PasteAssignments(division, group, table, division.Subject, markup);
                //TODO: PasteAssignmentProgress когда будет готов доступ к ним из StudyEntityService
            }

            table.FormatSheets(markup);
        }

        public void UpdateDivision(Division division, ISheetMarkup? markup = null)
        {
            throw new NotImplementedException();
        }

        private void PasteStudents(Division division, StudyGroup group, TTable table, ISheetMarkup markup)
        {
            TableIndex index = markup.StudentsStartIndex.WithSheet(group.Name, division.Groups.IndexOf(group));

            table.SetValuesForCellsAt(
                index,
                new List<List<string>>
                {
                    group.Students.Select(s => s.FullName).ToList()
                });
        }

        private void PasteAssignments(Division division, StudyGroup group, TTable table, Subject subject,
            ISheetMarkup markup)
        {
            TableIndex index = markup.AssignmentStartIndex.WithSheet(group.Name, division.Groups.IndexOf(group));

            foreach (StudyAssignment assignment in subject.Assignments)
            {
                table.SetValueForCellAt(
                    index,
                    assignment.Title);

                table.SetValueForCellAt(
                    index
                        .WithRow(markup.AssignmentStartIndex.Row + markup.CellsPerAssignmentTitle.height),
                    assignment.MinPoints);
                table.SetValueForCellAt(
                    index
                        .WithColumn(index.Column + markup.CellsPerAssignmentTitle.width / 2)
                        .WithRow(markup.AssignmentStartIndex.Row + markup.CellsPerAssignmentTitle.height),
                    assignment.MinPoints);

                index.Column += markup.CellsPerAssignmentTitle.width;
            }
        }

        private void PasteAssignmentProgress(Division division, StudyGroup group, TTable table,
            List<StudentAssignmentProgress> progresses, Subject subject, ISheetMarkup markup)
        {
            foreach (StudentAssignmentProgress progress in progresses)
            {
                int student = group.Students.FindIndex(s => s.SystemId == progress.Student.SystemId);
                int assignment = subject.Assignments.FindIndex(a => a.SystemId == progress.Assignment.SystemId);

                if (student == -1 || assignment == -1)
                    throw new InvalidDataException();

                var index = new TableIndex(
                    group.Name,
                    division.Groups.IndexOf(group),
                    markup.AssignmentStartIndex.Column + assignment,
                    markup.StudentsStartIndex.Row + student);

                table.SetValueForCellAt(index, progress.Progress.Points);
            }
        }
    }
}