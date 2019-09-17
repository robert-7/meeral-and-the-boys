import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

router.get('/', (req, res) => {
    return res.send(Object.values(req.context.models.planes));
  });

router.put('/', (req, res) => {
  const id = uuidv4();

  const newPlane = {
    id,
    lives: 20,
    coord: [
        Math.floor(Math.random() * 100) + 1,
        Math.floor(Math.random() * 100) + 1,
        Math.floor(Math.random() * 100) + 1
    ],
    rotation: 90,
  };

  req.context.models.planes[id] = newPlane;
  return res.send(newPlane);
});

router.post('/:planeID', (req, res) => {
    var plane = req.context.models.planes[req.params.planeID];
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
