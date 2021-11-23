using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SeaInk.Endpoints.Sdk;

namespace SeaInk.Endpoints.Client.Pages.TableGeneration
{
    public partial class TableGenerationPage
    {
        [Inject]
        public MentorClient MentorClient { get; set; }
        [Inject]
        public SubjectClient SubjectClient { get; set; }

        private MentorDto _currentMentor;

        private IReadOnlyList<SubjectDto> _subjects;
        private int _selectedSubjectId;

        private IReadOnlyList<StudyGroupDto> _groups;
        private IReadOnlyList<StudyGroupSubjectDto> _studyGroupSubjects;

        private string _selectedTab;

        public TableGenerationPage()
        {
            _selectedTab = "group";
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentMentor = await MentorClient.CurrentAsync();
            _subjects = _currentMentor.StudyGroupSubjects.Select(sgs => sgs.Subject).ToList();
            _groups = new List<StudyGroupDto>();
            _studyGroupSubjects = new List<StudyGroupSubjectDto>();

            if (_subjects.Count != 0)
                OnSelectedSubjectChanged(_subjects[0].Id);
        }

        private void OnSelectedTabChanged(string name)
        {
            _selectedTab = name;
        }

        private void OnSelectedSubjectChanged(int subjectId)
        {
            _selectedSubjectId = subjectId;

            _studyGroupSubjects = _currentMentor
                .StudyGroupSubjects
                .Where(sgs => sgs.Subject.Id == _selectedSubjectId)
                .ToList();

            _groups = _studyGroupSubjects
                .Select(sgs => sgs.StudyGroup)
                .GroupBy(g => g.Id)
                .Select(gg => gg.First())
                .ToList();

            if (_groups.Count == 0)
                return;

            _groups = _groups.OrderBy(g => g.Name).ToList();
        }

        public async Task GenerateTable(int groupId)
        {
            await SubjectClient.GenerateTableAsync(_selectedSubjectId, groupId);
        }
    }
}