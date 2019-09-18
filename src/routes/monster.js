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

router.put('/', (req, res) => {
    var monster = req.context.models.master.monster;
    if (!!monster) {
        const monsterUpdate = req.body
        for (var key in monsterUpdate) { monster[key] = monsterUpdate[key]; }
        // when a collision event occurs, decrement the lives of the planeIDCollifed
        if (!!monsterUpdate["event"] && monsterUpdate["event"]["type"] == "collision") {
            const planeID = monsterUpdate["event"]["planeID"];
            const plane = req.context.models.master.planes[planeID];
            if (!!plane) {
                plane["lives"]--;
                // handle plane death
                if (plane["lives"] === 0) {
                    delete req.context.models.master.planes[planeID];
                }
                else {
                    plane["planeDeath"] = Date.now();
                    plane["status"] = "dead";
                }
            }
            else {
                console.log('plane not found');
                return res.send('plane not found');
            }
        }
    }
    else{
        console.log('monster not found')
        return res.send('monster not found');
    }
        
    return res.send(req.context.models.master);
  });

export default router;
