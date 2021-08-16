using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Client.Controllers
{
    public class MentorControllerClient
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MentorControllerClient(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
        {
            _client = client;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public Task<MentorDto> GetMentorAsync(int mentorId)
            => GetValueAsync<MentorDto>($"/mentors/{mentorId}");

        public Task<IReadOnlyList<SubjectDto>> GetSubjectsAsync(int mentorId)
            => GetValueAsync<IReadOnlyList<SubjectDto>>($"/mentors/{mentorId}/subjects");

        public Task<IReadOnlyList<DivisionDto>> GetDivisionsAsync(int mentorId, int subjectId)
            => GetValueAsync<IReadOnlyList<DivisionDto>>($"/mentors/{mentorId}/subjects/{subjectId}/divisions");

        public Task<IReadOnlyList<StudyGroupDto>> GetGroupsListAsync(int mentorId, int subjectId, int divisionId)
            => GetValueAsync<IReadOnlyList<StudyGroupDto>>($"/mentors/{mentorId}/subject/{subjectId}/divisions/{divisionId}/groups");

        private async Task<T> GetValueAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }
    }
}