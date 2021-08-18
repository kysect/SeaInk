using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SeaInk.Endpoints.Client.Controllers;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Client.Pages.Groups
{
    public partial class GroupsPage
    {
        [Inject]
        public MentorControllerClient Controller { get; set; }

        private MentorDto _currentMentor;

        private IReadOnlyList<SubjectDto> _subjects;
        private SubjectDto _selectedSubject;

        private IReadOnlyList<DivisionDto> _divisions;
        private DivisionDto _selectedDivision;

        private IReadOnlyList<StudyGroupDto> _groups;
        private string _selectedGroupId;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentMentor = await Controller.GetCurrentMentorAsync();
            _subjects = await Controller.GetSubjectsAsync(_currentMentor.Id);

            if (_subjects.Count != 0)
                OnSelectedSubjectChangedAsync(_subjects[0].Id);
        }

        private void OnSelectedSubjectChangedAsync(int subjectId)
        {
            _selectedSubject = _subjects.Single(s => s.Id == subjectId);
            _divisions = _currentMentor.Divisions.Where(d => d.Subject.Id == subjectId).ToList();
            if (_divisions.Count != 0)
                OnSelectedDivisionChanged(_divisions[0].Id);
        }

        private void OnSelectedDivisionChanged(int divisionId)
        {
            _selectedDivision = _divisions.Single(d => d.Id == divisionId);
            _groups = _selectedDivision.Groups;
            if (_groups.Count != 0)
                OnSelectedGroupChanged(_groups[0].Id.ToString());
        }

        private void OnSelectedGroupChanged(string groupId)
        {
            _selectedGroupId = groupId;
        }
    }
}