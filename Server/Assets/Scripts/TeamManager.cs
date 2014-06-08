using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public class TeamManager : ITeamManager
{
    private int numberOfTeams;
    public int NumberOfTeams
    { 
        get
        {
            return numberOfTeams;
        }
    }

    private ITeam[] teams;
    public IEnumerable<ITeam> Teams
    {
        get
        {
            return teams;
        }
    }

    public TeamManager(ITeam[] teams)
    {
        if (teams.Count() == 0)
        {
            throw new ArgumentException("No teams provided");
        }

        if (teams.Contains(null))
        {
            throw new ArgumentException("Null provided as team object");
        }

        this.numberOfTeams = teams.Count();
        this.teams = teams;
    }

    public void AddPlayer(IPlayer player)
    {
        int minSize = teams.Min(t => t.Size);
        IEnumerable<ITeam> minTeams = teams.Where(t => t.Size == minSize);
        ITeam minTeam = minTeams.Aggregate((min, next) => min.Progress < next.Progress ? min : next);
        minTeam.AddPlayer(player);
    }

    public void RemovePlayer(IPlayer player)
    {
        ITeam team = player.Team;
        if (team != null && teams.Contains(team))
        {
            team.RemovePlayer(player);
        }
    }

    public IPlayer GetPlayer(INetworkPlayer networkPlayer)
    {
        foreach (ITeam team in teams)
        {
            foreach (IPlayer player in team.Players)
            {
                if (player.NetworkPlayer.Equals(networkPlayer))
                {
                    return player;
                }
            }
        }
        return null;
    }
}
