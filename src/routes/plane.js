import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

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
    createTime : Date.now()
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
    }
    else{
        console.log('plane not found');
        return res.send('plane not found');
    }
        
    return res.send(req.context.models.master);
  });

export default router;
