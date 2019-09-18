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
    deathTime: 0
  };

  req.context.models.master.planes[id] = newPlane;
  return res.send(newPlane);
});

export default router;
