# Api documentation

With this api you can access the authorization server

## Domain

```http request
http://localhost:5213
```

# Contents

- #### [Register](#Register);
- #### [Login](#Login);
- #### [Refresh](#Refresh);
- #### [Logout](#Logout);
- #### [Change user name](#ChangeName);
- #### [Change password](#ChangePassword);

## <a name="Register"></a> Register

### Request

Registers the user in the system and sends an email to confirm the email

```http request
POST api/auth/register
```

#### Body request

| Parameter       |  Type  |  Is Required |
|-----------------|:------:|-------------:|
| Name            | string | **Required** |
| Password        | string | **Required** |
| ConfirmPassword | string | **Required** |
| Email           | string | **Required** |

### Response

`OK` User is successfully created. Email confirmation message sent to your email address

#### Response codes

| Code |     Type      |                           When it is sent |
|------|:-------------:|------------------------------------------:|
| 400  | `BAD REQUEST` | Unable to create a user or send a message |
| 200  |     `OK`      |             User created and message sent |

## <a name="Login"></a> LogIn

### Request

Returns access and refresh tokens to the user only if the user has confirmed his email

```http request
POST api/auth/login
```

#### Body request

| Parameter |  Type  |  Is Required |
|-----------|:------:|-------------:|
| Email     | string | **Required** |
| Password  | string | **Required** |

### Response

```json
{
  "tokenAccess": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5OGNlZWQyMC03OTgwLTRlMTItOWYxOC1hNTRlZDhjMDQ3MmMiLCJlbWFpbCI6ImJAbWFpbC5ydSIsIm5hbWUiOiJhbW9nIiwibmJmIjoxNjYyNTMwNDYzLCJleHAiOjE2NjI1MzA1MjMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.tnIGKDXjxhvvZX-ytYoHsgOESTXSd8JESHh-4QnmZYU",
    "expireTime": "2022-09-07T06:02:03.3486792Z"
  },
  "tokenRefresh": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NjI1MzA0NjMsImV4cCI6MTY2MjUzNDA2MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.nfZzot_1G7BkvLdY1yzWI84pGUpzK04X8eScbYW6GGw",
    "expireTime": "2022-09-07T07:01:02.9907544Z"
  }
}
```

#### Access Token Contains:

- id
- email
- name

#### Response codes

| Code |     Type      |                                    When it is sent |
|------|:-------------:|---------------------------------------------------:|
| 400  | `BAD REQUEST` |           If there is an error in the request form |
| 404  |  `Not Found`  |                  No user with this email was found |
| 401  | `Unathorized` | Email is not verified or the password is incorrect |
| 200  |     `OK`      |                         The request was successful |

## <a name="Refresh"></a> Refresh

### Request

Used to get a new pair of access and refresh tokens

#### Body request

| Parameter    |  Type  |  Is Required |
|:-------------|:------:|-------------:|
| RefreshToken | string | **Required** |

### Response

```json
{
  "tokenAccess": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5OGNlZWQyMC03OTgwLTRlMTItOWYxOC1hNTRlZDhjMDQ3MmMiLCJlbWFpbCI6ImJAbWFpbC5ydSIsIm5hbWUiOiJhbW9nIiwibmJmIjoxNjYyNTMwNDYzLCJleHAiOjE2NjI1MzA1MjMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.tnIGKDXjxhvvZX-ytYoHsgOESTXSd8JESHh-4QnmZYU",
    "expireTime": "2022-09-07T06:02:03.3486792Z"
  },
  "tokenRefresh": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NjI1MzA0NjMsImV4cCI6MTY2MjUzNDA2MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.nfZzot_1G7BkvLdY1yzWI84pGUpzK04X8eScbYW6GGw",
    "expireTime": "2022-09-07T07:01:02.9907544Z"
  }
}
```

#### Response codes

| Code |     Type      |                                                          When it is sent |
|------|:-------------:|-------------------------------------------------------------------------:|
| 400  | `BAD REQUEST` |                                                         Token is invalid |
| 404  |  `Not Found`  | Unable to retrieve a user from a token or remove a token from a database |
| 200  |     `OK`      |                                               The request was successful |

## <a name="Logout"></a> LogOut

### Request

Used to log the user out of the system

```http request
POST api/auth/logout
```

#### Body request

| Parameter    |  Type  |  Is Required |
|:-------------|:------:|-------------:|
| RefreshToken | string | **Required** |

### Response

`OK` Logout successfully completed

#### Response codes

| Code |     Type      |            When it is sent |
|------|:-------------:|---------------------------:|
| 400  | `BAD REQUEST` |          Can't logout user |
| 200  |     `OK`      | The request was successful |

## <a name="ChangePassword"></a> Change Password

### Request

Used to change the password.
When the password is successfully changed, logout the user and sends a new pair of access and refresh tokens

```http request
POST api/account/change-password
```

#### Body request

| Parameter    |  Type  |  Is Required |
|:-------------|:------:|-------------:|
| Password     | string | **Required** |
| NewPassword  | string | **Required** |
| RefreshToken | string | **Required** |

### Response

```json
{
  "tokenAccess": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5OGNlZWQyMC03OTgwLTRlMTItOWYxOC1hNTRlZDhjMDQ3MmMiLCJlbWFpbCI6ImJAbWFpbC5ydSIsIm5hbWUiOiJhbW9nIiwibmJmIjoxNjYyNTMwNDYzLCJleHAiOjE2NjI1MzA1MjMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.tnIGKDXjxhvvZX-ytYoHsgOESTXSd8JESHh-4QnmZYU",
    "expireTime": "2022-09-07T06:02:03.3486792Z"
  },
  "tokenRefresh": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NjI1MzA0NjMsImV4cCI6MTY2MjUzNDA2MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.nfZzot_1G7BkvLdY1yzWI84pGUpzK04X8eScbYW6GGw",
    "expireTime": "2022-09-07T07:01:02.9907544Z"
  }
}
```

#### Response codes

| Code |     Type      |                           When it is sent |
|------|:-------------:|------------------------------------------:|
| 400  | `BAD REQUEST` | Token is invalid or can't change password |
| 404  |  `Not Found`  |         No user with this token was found |
| 200  |     `OK`      |                The request was successful |

## <a name="ChangeName"></a> Change User name

### Request

Used to change the user name.
When the user name is successfully changed, logout the user and sends a new pair of access and refresh tokens

```http request
POST api/account/change-username
```

#### Body request

| Parameter    |  Type  |  Is Required |
|:-------------|:------:|-------------:|
| NewUserName  | string | **Required** |
| RefreshToken | string | **Required** |

### Response

```json
{
  "tokenAccess": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5OGNlZWQyMC03OTgwLTRlMTItOWYxOC1hNTRlZDhjMDQ3MmMiLCJlbWFpbCI6ImJAbWFpbC5ydSIsIm5hbWUiOiJhbW9nIiwibmJmIjoxNjYyNTMwNDYzLCJleHAiOjE2NjI1MzA1MjMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIn0.tnIGKDXjxhvvZX-ytYoHsgOESTXSd8JESHh-4QnmZYU",
    "expireTime": "2022-09-07T06:02:03.3486792Z"
  },
  "tokenRefresh": {
    "tokenValue": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NjI1MzA0NjMsImV4cCI6MTY2MjUzNDA2MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.nfZzot_1G7BkvLdY1yzWI84pGUpzK04X8eScbYW6GGw",
    "expireTime": "2022-09-07T07:01:02.9907544Z"
  }
}
```

#### Response codes

| Code |     Type      |                            When it is sent |
|------|:-------------:|-------------------------------------------:|
| 400  | `BAD REQUEST` | Token is invalid or can't change user name |
| 404  |  `Not Found`  |          No user with this token was found |
| 200  |     `OK`      |                 The request was successful |
