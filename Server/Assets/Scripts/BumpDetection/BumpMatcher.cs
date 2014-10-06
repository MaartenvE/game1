using System;
using System.Collections.Generic;
using UnityEngine;
using BuildingBlocks.Player;

namespace BuildingBlocks.BumpDetection
{
    public class BumpMatcher : IBumpMatcher
    {
        const float BUMP_BACKLOG_TIME = 3f;
        const float MAX_BUMP_TIME = 0.5f;

        LinkedList<Bump> bumpHistory = new LinkedList<Bump>();

        public event BumpMatchHandler OnBumpMatch;

        public void Add(Bump newBump)
        {
            LinkedList<Bump> matches = findMatches(newBump);

            // If no matches were found, add the bump to the bump history so it can be matched to future incoming bumps.
            if (matches.Count == 0)
            {
                bumpHistory.AddFirst(newBump);
            }

            // If exactly one match was found, assume the match is correct. Remove the matched bump from the history to 
            // prevent re-matching.
            else if (matches.Count == 1)
            {
                if (OnBumpMatch != null)
                {
                    OnBumpMatch(matches.First.Value, newBump);
                }
            }

            // If more than one match was found, discard all matches.
            foreach (Bump match in matches)
            {
                bumpHistory.Remove(match);
            }
        }

        private LinkedList<Bump> findMatches(Bump bump)
        {
            LinkedList<Bump> matches = new LinkedList<Bump>();

            // Iterate through previous bumps
            LinkedListNode<Bump> node = bumpHistory.First;
            while (node != null)
            {
                var next = node.Next;
                var oldBump = node.Value;

                // Remove all entries older than the maximum age in the bump backlog.
                if (oldBump.Time < Network.time - BUMP_BACKLOG_TIME)
                {
                    bumpHistory.Remove(oldBump);
                }

                // Remove all old entries from this sender.
                else if (oldBump.Sender.Equals(bump.Sender))
                {
                    bumpHistory.Remove(oldBump);
                }

                // Check if two bumps are within the maximum time in between bumps.
                else if (Math.Abs(oldBump.Time - bump.Time) <= MAX_BUMP_TIME)
                {
                    //Player.Player player = new Player.Player(bump.Sender);
                    //Player.Player other = new Player.Player(oldBump.Sender);
                    //if (player.Team.TeamId == other.Team.TeamId || player.Team.Size < 2 || other.Team.Size < 2)
                    //{
                        matches.AddFirst(oldBump);
                    //}
                }

                node = next;
            }

            return matches;
        }
    }
}
