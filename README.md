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

* visit http://localhost:3000
  * /messages
  * /messages/1
  * /users
  * /users/1

### Beyond GET Routes

#### CURL

* Create a message with:
  * `curl -X POST -H "Content-Type:application/json" http://localhost:3000/messages -d '{"text":"Hi again, World"}'`
* Delete a message with:
  * `curl -X DELETE -H "Content-Type:application/json" http://localhost:3000/messages/1`

#### Postman

* Install [Postman](https://www.getpostman.com/apps) to interact with REST API
* Create a message with:
  * URL: http://localhost:3000/messages
  * Method: POST
  * Body: raw + JSON (application/json)
  * Body Content: `{ "text": "Hi again, World" }`
* Delete a message with:
  * URL: http://localhost:3000/messages/1
  * Method: DELETE

### PLANE
* curl -X PUT localhost:3000/planes
  * {"id":"6430d87c-289c-4b2d-b035-5c3b959b35a0","lives":20,"coord":[20,20,44],"rotation":90}
* curl localhost:3000/planes
  * [{"id":"6430d87c-289c-4b2d-b035-5c3b959b35a0","lives":20,"coord":[20,20,44],"rotation":90}]
* curl -X POST -H "Content-Type:application/json" http://localhost:3000/planes/6430d87c-289c-4b2d-b035-5c3b959b35a0 -d '{"lives":"3", "rotation":10}'
  * {"id":"6430d87c-289c-4b2d-b035-5c3b959b35a0","lives":"3","coord":[20,20,44],"rotation":10}