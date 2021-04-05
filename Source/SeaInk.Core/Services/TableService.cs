using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SeaInk.Core.Entities;
using SeaInk.Core.Entities.Tables;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Styles;

namespace SeaInk.Core.Services
{
    public class TableService<TTable> where TTable : ITable, new()
    {
        //TODO: Придумать как быть с Division у которого уже есть TableId
        public void CreateTableForDivision(Division division, ISheetMarkup? markup = null)
        {
            markup ??= new DefaultSheetMarkup();

            var table = new TTable();
            division.TableId = table.Create();

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
                table.CreateSheet(group.Name);
                PasteStudents(group, table, markup);
                PasteAssignments(group, division.Subject, table, markup);
                //TODO: PasteAssignmentProgress когда будет готов доступ к ним из StudyEntityService
            }
            
            table.FormatSheets(markup);
        }

        public void UpdateDivision(Division division, ISheetMarkup? markup = null)
        {
            throw new NotImplementedException();
        }

        private void PasteStudents(StudyGroup group, TTable table, ISheetMarkup markup)
        {
            var index = new TableIndex(
                group.Name,
                markup.StudentsStartIndex.Column,
                markup.StudentsStartIndex.Row);

            table.SetValuesForCellsAt(
                index,
                group.Students.Select(s => s.FullName).ToList(),
                ITable.Direction.Vertical);
        }

        private void PasteAssignments(StudyGroup group, Subject subject, TTable table, ISheetMarkup markup)
        {
            int i = markup.AssignmentStartIndex.Column;

            foreach (StudyAssignment assignment in subject.Assignments)
            {
                table.SetValueForCellAt(new TableIndex(
                        group.Name,
                        i,
                        markup.AssignmentStartIndex.Row),
                    assignment.Title);
                
                table.SetValueForCellAt(new TableIndex(
                    group.Name,
                    i,
                    markup.AssignmentStartIndex.Row + markup.CellsPerAssignmentTitle.height),
                    assignment.MinPoints);
                table.SetValueForCellAt(new TableIndex(
                        group.Name,
                        i + markup.CellsPerAssignmentTitle.width / 2,
                        markup.AssignmentStartIndex.Row + markup.CellsPerAssignmentTitle.height),
                    assignment.MinPoints);

                i += markup.CellsPerAssignmentTitle.width;
            }
        }
        
        private void PasteAssignmentProgress(List<StudentAssignmentProgress> progresses, Subject subject, StudyGroup group, TTable table,
            ISheetMarkup markup)
        {
            foreach (StudentAssignmentProgress progress in progresses)
            {
                int student = group.Students.FindIndex(s => s.SystemId == progress.Student.SystemId);
                int assignment = subject.Assignments.FindIndex(a => a.SystemId == progress.Assignment.SystemId); 

                if (student == -1 || assignment == -1)
                    throw new InvalidDataException();

                var index = new TableIndex(
                    group.Name,
                    markup.AssignmentStartIndex.Column + assignment,
                    markup.StudentsStartIndex.Row + student);
                
                table.SetValueForCellAt(index, progress.Progress.Points);
            }
        }
    }
}