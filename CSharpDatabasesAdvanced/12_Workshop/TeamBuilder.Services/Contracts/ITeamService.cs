using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services.Contracts
{
    public interface ITeamService
    {
        TModel CreateTeam<TModel>(params string[] args);

        string InviteToTeam(string teamName, string userName);

        string AcceptInvite(string teamName);

        string DeclineInvite(string teamName);

        string KickMember(string teamName, string userName);

        string Disband(string teamName);

        string AddTeamTo(string eventName,string teamName);

        string ShowTeam(string teamName);

    }
}
