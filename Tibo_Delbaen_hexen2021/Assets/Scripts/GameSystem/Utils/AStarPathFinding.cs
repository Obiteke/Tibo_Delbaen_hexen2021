using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace GameSystem.Utils
{
    public class AStarPathFinding<TPosition>
    {
        public delegate List<TPosition> NeighbourStrategy(TPosition from);
        public delegate float DistanceStrategy(TPosition from, TPosition toNeighbour);
        public delegate float HeuristicStrategy(TPosition from, TPosition to);

        private readonly NeighbourStrategy _neighbours;
        private readonly DistanceStrategy _distance;
        private readonly HeuristicStrategy _heuristic;

        public AStarPathFinding(NeighbourStrategy neighbours, DistanceStrategy distance, HeuristicStrategy heuristic)
        {
            _neighbours = neighbours;
            _distance = distance;
            _heuristic = heuristic;
        }

        public List<TPosition> Path(TPosition from, TPosition to)
        {
            var openSet = new List<TPosition> { from };

            var cameFrom = new Dictionary<TPosition, TPosition>();

            var gScores = new Dictionary<TPosition, float>() { { from, 0 } };

            var fScores = new Dictionary<TPosition, float>() { { from, _heuristic(from, to) } };

            while (openSet.Count != 0)
            {
                TPosition current = FindLowestFScore(fScores, openSet);

                if (current.Equals(to))
                    return ReconstructPath(cameFrom, current);
                //return gScores.Keys.ToList(); // Returns all tried tiles

                openSet.Remove(current);

                var neighbours = _neighbours(current);
                foreach (var neighbour in neighbours)
                {
                    var tentativeGScore = gScores[current] + _distance(current, neighbour);

                    if (tentativeGScore < gScores.GetValueOrDefault(neighbour, float.PositiveInfinity))
                    {
                        cameFrom[neighbour] = current;
                        gScores[neighbour] = tentativeGScore;
                        fScores[neighbour] = gScores[neighbour] + _heuristic(neighbour, to);

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return new List<TPosition>(0);
        }

        private List<TPosition> ReconstructPath(Dictionary<TPosition, TPosition> cameFrom, TPosition current)
        {
            var path = new List<TPosition>() { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            return path;
        }

        public TPosition FindLowestFScore(Dictionary<TPosition, float> fScores, List<TPosition> openSet)
        {
            TPosition currentNode = openSet[0];

            foreach (var node in openSet)
            {
                var currentFScore = fScores.GetValueOrDefault(currentNode, float.PositiveInfinity);
                var fScore = fScores.GetValueOrDefault(node, float.PositiveInfinity);
                if (fScore < currentFScore)
                    currentNode = node;
            }

            return currentNode;
        }
    }
}
