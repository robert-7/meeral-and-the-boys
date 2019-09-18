import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();
const respawnTime = 3;

router.get('/', (req, res) => {
    return res.send(req.context.models.master);
  });

router.post('/', (req, res) => {
  const id = uuidv4();

  const newPlane = {
    id,
    lives: 20,
    rotation: [ // y-rotation - [0-359], z-rotation - [0-359])
        Math.floor(Math.random() * 360) + 1,
        Math.floor(Math.random() * 360) + 1
    ],
    status: 'alive',
    deathTime: 0
  };

  req.context.models.master.planes[id] = newPlane;
  return res.send(newPlane);
});

router.put('/', (req, res) => {
    var master = req.context.models.master;

    // If it's a plane update request...
    if (!!req.body.plane) {
      const planeUpdates = req.body.plane;
      const planeID = req.body.plane.id;

      if (master.planes[planeID] !== undefined) {

        // Respawn logic.
        if (master.planes[planeID].deathTime !== 0) {
          if (Date.now() - master.planes[planeID].deathTime > 3000) {
            master.planes[planeID].deathTime = 0;
            master.planes[planeID].status = 'alive'
          }
        }

        for (var key in planeUpdates) { // Update typical plane data...
          master.planes[planeID][key] = planeUpdates[key];
        }
      } else {
        console.log('plane not found: ' + planeID);
        return res.send('plane not found: ' + planeID);
      }
    }

    // If it's a monster update request...
    if (!!req.body.monster) {
      const monsterUpdates = req.body.monster;

      if (master.monster !== undefined) {
        for (var key in monsterUpdates) { // Update typical monster data...
          master.monsterUpdates[key] = monsterUpdates[key];
        }
      } else {
        console.log('monster not found');
        return res.send('monster not found');
      }
    }
     // Updating game events.
    for (var i = 0; i < req.body.events.length; i++) {
      var newEvent = req.body.events[i];
      var eventPlaneID = newEvent.planeID;
      var event = {};

      if (newEvent.type === 1) { // shooting event, update monster health.
        master.monster.health--;
      } else if (newEvent.type === 2) { // hit even, update plane life.
        if (master.planes[eventPlaneID] !== undefined) {
          master.planes[eventPlaneID].lives--; // minus plane life
          if (master.planes[eventPlaneID].lives === 0) { // If 0, delete plane permanently.
            delete req.context.models.master.planes[eventPlaneID];
          } else {
            plane.status = "dead";
          }
        } else {
          console.log('plane not found: ' + eventPlaneID);
          return res.send('plane not found: ' + eventPlaneID);
        }
      }

      for (var key in newEvent) { // Copying event obj from request to master obj.
        event[key] = newEvent[key];
      }
      event.timeStamp = Date.now(); // Tag the event with a timestamp.
      master.events.push(event); // Push event to events list in master obj.
    }
    return res.send(req.context.models.master);
  });

export default router;
