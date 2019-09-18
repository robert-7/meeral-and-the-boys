import uuidv4 from 'uuid/v4';
import { Router } from 'express';

const router = Router();

const EVENT_ENUM = {
    0 : 'collision',
    1 : 'hit'
}

router.put('/', (req, res) => {

    // update plane state
    if (!!selfPlane) {
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
        }
            
        return res.send(req.context.models.master);
    }

    // update monster state
    if (!!selfMonster) {
        var monster = req.context.models.master.monster;
        if (!!monster) {
            const monsterUpdate = req.body
            for (var key in monsterUpdate) { 
                monster[key] = monsterUpdate[key]; 
            }
        }
        else{
            console.log('monster not found');
        }
    }

    // manage new events eventsOccured[]
    if (!!eventsOccured && eventsOccure.length > 0) {

        for (var event in eventsOccured) {
            req.context.models.master.events.push(event);

            if (event["eventCode"] == 0) {
                const planeID = event["planeID"];
                const plane = req.context.models.master.planes[planeID];
                if (!!plane) {
                    plane["lives"]--;
                    if (plane["lives"] === 0) {
                        delete req.context.models.master.planes[planeID];
                    }
                    else {
                        plane["status"] = "dead";
                    }
                }
                else {
                    console.log('plane not found');
                    return res.send('plane not found');
                }
            }
            else if (event["eventCode"] == 1) {
                const monster = req.context.models.master.monster;
                if (!!monster) {

                    // remove monster life when hit
                    monster["lives"]--;
                    if (monster["lives"] === 0) {
                    delete req.context.models.master.monster; // goodbye
                    }

                    // remove shooting event from plane that shot
                    const planeID = event["planeID"];
                    const plane = req.context.models.master.planes[planeID];
                    if (!!plane) {
                    delete req.context.models.master.planes[planeID].event;
                    }
                    else {
                        plane["status"] = "dead";
                    }
                    
                }
                else {
                    console.log('monster not found');
                    return res.send('monster not found');
                }
            }
            else {
                console.log('unknown event: ' + event["type"]);
                return res.send('unknown event: ' + event["type"]);
            }
        }
    }
        
    return res.send(req.context.models.master);
  });

export default router;
