using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_DestinationPointsList {

    List<AI_DestinationPoint> freeDestinationPoint;
    List<AI_DestinationPoint> occupiedDestinationPoint = new List<AI_DestinationPoint>();

    public AI_DestinationPointsList(AI_DestinationPoint[] array) {
        freeDestinationPoint = new List<AI_DestinationPoint>(array);
    }

    public AI_DestinationPoint GetRandomPoint(AI_DestinationPoint currentPoint) {
        List<AI_DestinationPoint> destinations = new List<AI_DestinationPoint>();
        for (int i = 0; i < freeDestinationPoint.Count; i++) {
            if (freeDestinationPoint[i].isActive)
                destinations.Add(freeDestinationPoint[i]);
        }
        AI_DestinationPoint destination = destinations[Random.Range(0, destinations.Count)];
        //Change Lists
        if (occupiedDestinationPoint.Contains(currentPoint)) {
            RemovePointToOccupied(currentPoint);
        }
        AddPointToOccupied(destination);
        return destination;
    }

    void AddPointToOccupied(AI_DestinationPoint destination) {
        occupiedDestinationPoint.Add(destination);
        freeDestinationPoint.Remove(destination);
        destination.isOccupied = true;
    }

    void RemovePointToOccupied (AI_DestinationPoint currentPoint) {
        occupiedDestinationPoint.Remove(currentPoint);
        freeDestinationPoint.Add(currentPoint);
        currentPoint.isOccupied = false;
    }
}
