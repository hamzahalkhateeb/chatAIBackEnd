/*
1- get current loggin user profile
//get request comes in
//check the token and verify it
//get the user id from the token
//retrieve the details and send it back

2- get another users public profile
//get request comes in, includes the requested user id
// check the access token if its valid
// retrieve user data and send it back

3- search the users table to start a new chat
// get request comes it includes username and token
//verify token
//use fuzzy search to look for users with similar user names
//send top 3 results

4- update current logged in users profile
//patch request comes in, includes fields which needs changing
//verify token and ensure the to be edited user id matches the id in the request
//execute the changes and send back success

5- deactivate or delete own profile
//delete request comes in with a user id
//verify token and make sure its the same id as in the request
//set the disabled at date and time

6- pin user
//post request comes in with user id, and chat id
//verify token, ensure the id in the token is also in the chat members table
//set the pinned user fiel of the chat to that user id



*/
