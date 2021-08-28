using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MoreLinq;
using SeaInk.Endpoints.Client.Client;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Client.Pages.Groups
{
    public partial class GroupsPage
    {
        [Inject]
        public MentorClient Client { get; set; }

        private MentorDto _currentMentor;
        
        private IReadOnlyList<SubjectDto> _subjects;
        private int _selectedSubjectId;

        private IReadOnlyList<DivisionDto> _divisions;
        private int _selectedDivisionId;

        private IReadOnlyList<StudyGroupDto> _groups;
        private string _selectedGroupId;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentMentor = await Client.GetCurrentMentorAsync();
            _divisions = await Client.GetDivisionsAsync(_currentMentor.Id);
            _subjects = _currentMentor.StudyGroupSubjects.Select(sgs => sgs.Subject).ToList();

            if (_subjects.Count != 0)
                OnSelectedSubjectChanged(_subjects[0].Id);
        }

        private void OnSelectedSubjectChanged(int subjectId)
        {
            _selectedSubjectId = subjectId;
            
            if (_divisions.Count != 0)
                OnSelectedDivisionChanged(_divisions[0].Id);
        }

        private void OnSelectedDivisionChanged(int divisionId)
        {
            _selectedDivisionId = divisionId;
            _groups = _divisions
                .Where(d => d.Id == divisionId)
                .SelectMany(d => d.StudyGroupSubjects)
                .Where(sgs => sgs.Subject.Id == _selectedSubjectId)
                .Select(sgs => sgs.StudyGroup)
                .DistinctBy(g => g.Id)
                .ToList();

            if (_groups.Count == 0)
                return;

            _groups = _groups.OrderBy(g => g.Name).ToList();
            OnSelectedGroupChanged(_groups[0].Id.ToString());
        }

        private void OnSelectedGroupChanged(string groupId)
        {
            _selectedGroupId = groupId;
        }
    }
}