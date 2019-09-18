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
    createTime : Date.now(),
    deathTime: 0
  };

  req.context.models.master.planes[id] = newPlane;
  return res.send(newPlane);
});

router.put('/:planeID', (req, res) => {
    var plane = req.context.models.master.planes[req.params.planeID];
    console.log(plane);
    if (!!plane) {
      const planeUpdate = req.body

      for (var key in planeUpdate) {
        plane[key] = planeUpdate[key];
      }
      // handle respawn
      if (plane["status"] === "dead") {
        console.log("is dead");
        var timeSinceDied = Date.now() - plane["deathTime"];
        console.log("deathTime is " + plane["deathTime"] + " and Date.now() is " + Date.now() + " so minus is " + timeSinceDied);
        if (timeSinceDied/1000 >= respawnTime) {
          console.log("time to bring them back to life because " + timeSinceDied/1000);
          plane["status"] = "alive";
        }
      }
    }
    else{
        console.log('plane not found');
        return res.send('plane not found');
    }
        
    return res.send(req.context.models.master);
  });

export default router;
