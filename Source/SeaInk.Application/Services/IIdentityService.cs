using System.Threading.Tasks;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services;

public interface IIdentityService
{
    Task<Mentor> GetCurrentMentor();
}