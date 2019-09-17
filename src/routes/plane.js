import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

router.get('/', (req, res) => {
    return res.send(Object.values(req.context.models.master.planes));
  });

router.put('/', (req, res) => {
  const id = uuidv4();

  const newPlane = {
    id,
    lives: 20,
    coord: [ // (radius - [0-100], y-rotation - [0-359], z-rotation - [0-359])
        Math.floor(Math.random() * 100) + 1, 
        Math.floor(Math.random() * 359) + 1,
        Math.floor(Math.random() * 359) + 1
    ],
    rotation: 90,
  };

  req.context.models.master.planes[id] = newPlane;
  return res.send(newPlane);
});

router.post('/:planeID', (req, res) => {
    var plane = req.context.models.master.planes[req.params.planeID];
    if (!!plane) {
        const planeUpdate = req.body
        for (var key in planeUpdate) { plane[key] = planeUpdate[key]; }
    }
    else{
        console.log('plane not found')
        return res.send('plane not found');
    }
        
    return res.send(plane);
  });

export default router;
