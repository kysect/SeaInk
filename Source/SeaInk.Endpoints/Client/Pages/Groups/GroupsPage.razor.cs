using System;
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
        
        private StudyGroupDto _selectedGroup;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _subjects = await GetSubjects();
            _selectedSubject = _subjects[0];
            _currentMentor = await Controller.GetMentorAsync(0);
        }

        private async Task OnSelectedSubjectChanged(SubjectDto subject)
        {
            _divisions = await GetDivisions(subject.Id);
        }
        
        private Task<IReadOnlyList<SubjectDto>> GetSubjects()
            => Controller.GetSubjectsAsync(_currentMentor.Id);

        private Task<IReadOnlyList<DivisionDto>> GetDivisions(int subjectId)
            => Controller.GetDivisionsAsync(_currentMentor.Id, subjectId);
    }
}