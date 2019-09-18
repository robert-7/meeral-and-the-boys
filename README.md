```
13:44:43 [owen.sawyer:~] $ curl -X POST localhost:8080/planes
{
  "id": "30fd040e-3736-4044-ae4d-ba9f732ed936",
  "lives": 20,
  "rotation": [
    209,
    350
  ],
  "status": "alive",
  "deathTime": 0
}

13:44:53 [owen.sawyer:~] $ curl -X POST localhost:8080/monster
{
  "health": 5,
  "lh": [
    -10,
    0,
    0
  ],
  "lhRotation": [
    0,
    0,
    0
  ],
  "rh": [
    10,
    0,
    0
  ],
  "rhRotation": [
    0,
    0,
    0
  ],
  "head": [
    0,
    0,
    0
  ],
  "headRotation": [
    0,
    0,
    0
  ]
}

13:44:58 [owen.sawyer:~] $ curl -X PUT -H "Content-Type:application/json" http://localhost:8080/update -d '{"plane": {"id":"30fde4d-ba9f732ed936","lives":19,"rotation":[100,150],"status":"alive","deathTime":0},"events": [{"planeID": "315827f5-e24b-45ab-99c6-ff0171e943a6", "type": 1}]}'
{"planes":{"30fd040e-3736-4044-ae4d-ba9f732ed936":{"id":"30fd040e-3736-4044-ae4d-ba9f732ed936","lives":19,"rotation":[100,150],"status":"alive","deathTime":0}},"monster":{"health":4,"lh":[-10,0,0],"lhRotation":[0,0,0],"rh":[10,0,0],"rhRotation":[0,0,0],"head":[0,0,0],"headRotation":[0,0,0]},"events":[{"planeID":"315827f5-e24b-45ab-99c6-ff0171e943a6","type":1,"timeStamp":1568828732666}]}

13:45:34 [owen.sawyer:~] $ curl localhost:8080/planes
{
  "planes": {
    "30fd040e-3736-4044-ae4d-ba9f732ed936": {
      "id": "30fd040e-3736-4044-ae4d-ba9f732ed936",
      "lives": 19,
      "rotation": [
        100,
        150
      ],
      "status": "alive",
      "deathTime": 0
    }
  },
  "monster": {
    "health": 4,
    "lh": [
      -10,
      0,
      0
    ],
    "lhRotation": [
      0,
      0,
      0
    ],
    "rh": [
      10,
      0,
      0
    ],
    "rhRotation": [
      0,
      0,
      0
    ],
    "head": [
      0,
      0,
      0
    ],
    "headRotation": [
      0,
      0,
      0
    ]
  },
  "events": [
    {
      "planeID": "315827f5-e24b-45ab-99c6-ff0171e943a6",
      "type": 1,
      "timeStamp": 1568828732666
    }
  ]
}
```

### PLANE
#### Create Plane
* curl -X POST localhost:8080/planes 
  * {"id":"e2631b75-ea2d-4928-a727-262e4f3b0ad8","lives":20,"rotation":[271,222],"status":"alive","createTime":1568817952178}
#### Update Plane
* curl -X PUT -H "Content-Type:application/json" http://localhost:8080/planes/e2631b75-ea2d-4928-a727-262e4f3b0ad8 -d '{"lives":3, "rotation":[250,220]}'
  * {"planes":{"e2631b75-ea2d-4928-a727-262e4f3b0ad8":{"id":"e2631b75-ea2d-4928-a727-262e4f3b0ad8","lives":3,"rotation":[250,220],"status":"alive","createTime":1568817952178}},"monster":{}, event" : [{type: 1, rotation: [1,2]}]

#### Create Monster
* curl -X POST localhost:8080/monster 
  * {"health":5,"lh":[-10,0,0],"lhRotation":[0,0,0],"rh":[10,0,0],"rhRotation":[0,0,0],"head":[0,0,0],"headRotation":[0,0,0]}
#### Update Monster
** Events WIP
* curl -X PUT -H "Content-Type:application/json" http://104.197.210.110:8080/monster -d '{"event":"hit", "id":"e2631b75-ea2d-4928-a727-262e4f3b0ad8"}'
  * {"planes":{"e2631b75-ea2d-4928-a727-262e4f3b0ad8":{"id":"e2631b75-ea2d-4928-a727-262e4f3b0ad8","lives":3,"rotation":[250,220],"status":"alive","createTime":1568817952178}},"monster":{"health":5,"lh":[-10,0,0],"lhRotation":[0,0,0],"rh":[10,0,0],"rhRotation":[0,0,0],"head":[0,0,0],"headRotation":[0,0,0]}

### PROD
* npm run build
* npm run serve
* curl -X POST 104.197.210.110:8080/planes
* curl 104.197.210.110:8080/planes

# Simple Node with Express Server with REST API

[![Build Status](https://travis-ci.org/rwieruch/node-express-server-rest-api.svg?branch=master)](https://travis-ci.org/rwieruch/node-express-server-rest-api) [![Slack](https://slack-the-road-to-learn-react.wieruch.com/badge.svg)](https://slack-the-road-to-learn-react.wieruch.com/) [![Greenkeeper badge](https://badges.greenkeeper.io/rwieruch/node-express-server-rest-api.svg)](https://greenkeeper.io/)

An easy way to get started with a Express server offering a REST API with Node.js. [Read more about it.](https://www.robinwieruch.de/node-express-server-rest-api)

## Features

* Babel 7
* Environment Variables
* Express
* REST API

## Requirements

* [node & npm](https://nodejs.org/en/)
* [git](https://www.robinwieruch.de/git-essential-commands/)

## Installation

* `git clone git@github.com:rwieruch/node-express-server-rest-api.git`
* `cd node-express-server-rest-api`
* `npm install`
* `npm start`
* optional: include *.env* in your *.gitignore*

### GET Routes

* visit http://localhost:8080
  * /messages
  * /messages/1
  * /users
  * /users/1

### Beyond GET Routes

#### CURL

* Create a message with:
  * `curl -X POST -H "Content-Type:application/json" http://localhost:8080/messages -d '{"text":"Hi again, World"}'`
* Delete a message with:
  * `curl -X DELETE -H "Content-Type:application/json" http://localhost:8080/messages/1`

#### Postman

* Install [Postman](https://www.getpostman.com/apps) to interact with REST API
* Create a message with:
  * URL: http://localhost:8080/messages
  * Method: POST
  * Body: raw + JSON (application/json)
  * Body Content: `{ "text": "Hi again, World" }`
* Delete a message with:
  * URL: http://localhost:8080/messages/1
  * Method: DELETE
