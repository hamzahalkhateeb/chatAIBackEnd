
complete the log in endpoint
token generation and allat
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



