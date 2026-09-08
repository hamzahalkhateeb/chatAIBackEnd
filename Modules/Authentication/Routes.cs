/*
1- register user
//post request comes in
//contains password, email, username
//check if email exists, if yes, send error and redirect to log in page
//if no, validate all data is correct, then
//  create a user using email, username, hash password
//issue 2 tokens


2- log in
//post request comes in
//contains email and password
//look for email, if it doesnt exist, send error
//if email exists, hash password and compared hashes strings
//if hashed match, assign 2 tokens and send them to log in
//if no match, send error

3- refresh token
//post comes in, going to have acces token and refresh token, if refresh token is valid, recreate an access token and send it back

4- log out
//post request comes, with email
//check and verify access token, if the email matches the id present in the token, revoke that particular token
//if not, send an error message




*/