import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

router.get('/', (req, res) => {
    return res.send(Object.values(req.context.models.master.monster));
});

router.put('/', (req, res) => {
  const id = uuidv4();

  if (!!req.context.models.master.monster.id){
    return res.send(req.context.models.master.monster);
  }
  else{
    const newMonster = {
      id,
      health: 7 * 
        req.context.models.master.planes.length || 5,
      lh: [-10,0,0],
      lhRotation: [0,0,0],
      rh: [10,0,0],
      rhRotation: [0,0,0],
      head: [0,0,0],
      headRotation: [0,0,0],
    };
  
    req.context.models.master.monster = newMonster;
    return res.send(newMonster);
  }

});

router.post('/:monsterID', (req, res) => {
    var monster = req.context.models.master.monster[req.params.monsterID];
    if (!!monster) {
        const monsterUpdate = req.body
        for (var key in monsterUpdate) { monster[key] = monsterUpdate[key]; }
    }
    else{
        console.log('monster not found')
        return res.send('monster not found');
    }
        
    return res.send(monster);
  });

export default router;
