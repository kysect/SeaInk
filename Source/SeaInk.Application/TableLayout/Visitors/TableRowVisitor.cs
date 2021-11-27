using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Visitors
{
    public class TableRowVisitor : ITableRowVisitor
    {
        private readonly List<AssignmentProgressModel> _progresses;
        private StudentModel? _studentModel;

        public TableRowVisitor(TableRowModel row)
        {
            _studentModel = row.Student;
            _progresses = row.AssignmentProgresses.ToList();
        }

        public TableRowVisitor()
        {
            _progresses = new List<AssignmentProgressModel>();
        }

        public TableRowModel GetModel()
            => new TableRowModel(_studentModel.ThrowIfNull(nameof(_studentModel)), _progresses);

        public StudentModel GetStudent()
            => _studentModel.ThrowIfNull(nameof(_studentModel));

        public AssignmentProgress? GetProgress(AssignmentModel assignment)
            => _progresses.SingleOrDefault(p => p.Assignment.Equals(assignment))?.Progress;

        public void SetStudent(StudentModel student)
        {
            if (_studentModel is not null)
                throw new InvalidOperationException("Student model has been already set");

            _studentModel = student;
        }

        public void AddProgress(AssignmentModel assignment, AssignmentProgress progress)
            => _progresses.Add(new AssignmentProgressModel(assignment, progress));
    }
}