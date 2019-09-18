import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

router.get('/', (req, res) => {
    return res.send(req.context.models.master);
});

router.post('/', (req, res) => {

  if (!!req.context.models.master.monster.id){
    return res.send(req.context.models.master.monster);
  }
  else{
    const newMonster = {
      health: 7 * 
        req.context.models.master.planes.length || 5,
      lh: [-10,0,0],
      lhRotation: [0,0,0],
      rh: [10,0,0],
      rhRotation: [0,0,0],
      head: [0,0,0],
      headRotation: [0,0,0]
    };
  
    req.context.models.master.monster = newMonster;
    return res.send(newMonster);
  }

});

export default router;
