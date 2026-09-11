

check if tokens are working
complete the log in endpoint

when log in is complete:
    1- rehash password incoling password in controller, currently its not being hashed to allow for comparison
    2-fix IsCorrectPassword, currently its a plain comparison

move on to registration
registration endpoint

------------------------------------------------------

http rules:
 HTTP rules
Situation Response
Successful create 201 Created + resource/DTO
Successful update 200 OK
Successful delete 204 No Content
Validation failure 400 ProblemDetails with field errors
Unauthenticated 401
Authenticated but not permitted 403
Resource does not exist / hidden by policy 404
State conflict / stale version 409
Unexpected server failure 500 with correlation ID, no stack trace to client


-----------
any methods that touch the db would be in service files, any that dont touch the db are in helperMethods file


--------------------
missing features from existing code:
1- when user logs in, check if they already have a refresh token saved, if they do, give them that one

bugs to fix:
1- trailing spaces in email crash the app