using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SeaInk.Endpoints.Shared.Dto;

namespace SeaInk.Endpoints.Client.Client
{
    public class MentorClient: ClientBase
    {
        public MentorClient(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
            : base(client, jsonSerializerOptions) { }

        public Task<MentorDto> GetCurrentMentorAsync()
            => GetValueAsync<MentorDto>("/mentors/current");

        public Task<MentorDto> GetMentorAsync(int mentorId)
            => GetValueAsync<MentorDto>($"/mentors/{mentorId}");

        public Task<IReadOnlyList<SubjectDto>> GetSubjectsAsync(int mentorId)
            => GetValueAsync<IReadOnlyList<SubjectDto>>($"/mentors/{mentorId}/subjects");
    }
}