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

        private JsonSerializerOptions _jsonSerializerOptions;

        public MentorControllerClient(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
        {
            _client = client;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<List<SubjectDto>> GetSubjectsListAsync(int mentorId)
        {
            HttpResponseMessage response = await _client.GetAsync($"/Mentor/{mentorId}/subjects");

            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<SubjectDto>>(json, _jsonSerializerOptions);
        }

        public async Task<List<StudyGroupDto>> GetGroupsListAsync(int mentorId, int subjectId)
        {
            HttpResponseMessage response = await _client.GetAsync($"/Mentor/{mentorId}/subject/{subjectId}/groups");

            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<StudyGroupDto>>(json, _jsonSerializerOptions);
        }
    }
}