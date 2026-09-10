refresh token generation done
access token next
log in test via post man
cors settings
quick front end call from browser
check if tokens are working
complete the log in endpoint

move on to registration
registration endpoint

-----------------------------------------------------------

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

