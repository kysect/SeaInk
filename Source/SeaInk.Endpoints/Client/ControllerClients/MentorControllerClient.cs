using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Client.ControllerClients
{
    public class MentorControllerClient
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _caseInsensitiveOptions;

        public MentorControllerClient(HttpClient client)
        {
            _client = client;
            _caseInsensitiveOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<SubjectDto>> GetSubjectsListAsync(int mentorId)
        {
            HttpResponseMessage response = await _client.GetAsync($"/Mentor/{mentorId}/subjects");

            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");
            
            return JsonSerializer.Deserialize<List<SubjectDto>>(await response.Content.ReadAsStringAsync(),
                _caseInsensitiveOptions);
        }

        public async Task<List<StudyGroupDto>> GetGroupsListAsync(int mentorId, int subjectId)
        {
            HttpResponseMessage response = await _client.GetAsync($"/Mentor/{mentorId}/subject/{subjectId}/groups");
            
            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");
            
            return JsonSerializer.Deserialize<List<StudyGroupDto>>(await response.Content.ReadAsStringAsync(), _caseInsensitiveOptions);
        }
    }
}