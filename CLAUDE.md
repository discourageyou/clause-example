# App Name - CLAUDEEXAMPLE

**Global Standards** `%USERPROFILE%\.claude\GLOBAL_STANDARDS.md`
**Project Brief** `PROJECT_BREIF.md`

## Quick Reference

This file provides project-specific context for AI-assisted development.
Read GLOBAL_STANDARDS.md for general development principles - this file only covers what's unique to this project.

## Project Context

We need to implement two API endpoints:

1. A validation endpoint that accepts a kid’s number (in the circle) and a string representation of what they
   called out in the room
2. A collection endpoint that returns the sequence of kid numbers and correct responses that would be called
   out in the room

This test is designed to demonstrate your problem-solving skills and your aptitude with .NET. Please feel free to ask
questions, discuss your solution and use the internet if need be.
Your task is to implement the following kids counting game as a web API:
A group of kids are sitting in a circle. Going clockwise and starting at one they each count, calling out their number
to the room. In some scenarios, the kids will call out something other than their number:

1. Every time the kid’s number is divisible by 3, they must say "Star"
2. Every time the kid’s number is divisible by 5, they must say "Rez"
3. When the kid’s number is divisible by 3 and 5 at the same time, they must say "StarRez"

• The aim of this exercise isn't to finish quickly, but to show how you think about and structure a problem.
We'd rather you be 20% done and be able to talk through the decisions you're making then a rushed 100%
job.
• Pretend this API is something more important and will be used at large scale.
• Remember the ility's - readability, testability, maintainability....
• What happens if the API is used incorrectly? (e.g. invalid request body)

Sample Requests/Responses
Validate Endpoint
POST /validate
Request body:
{
"kidNumber": 4,
"kidResponse": "Star"
}
Response body (200 OK):
{
"isValid": false,
"expectedResponse": "4",
"errors": []
}
Collection endpoint
GET /all?from=1&to=100
Response body (200 OK):
{
"data": [
{
"kidNumber": 1,
"kidResponse": "1",
},
{
"kidNumber": 2,
"kidResponse": "2",
},
{
"kidNumber": 3,
"kidResponse": "Star",
},
{
"kidNumber": 4,
"kidResponse": "4",
},
{
"kidNumber": 5,
"kidResponse": "Rez",
},
....
],
"errors": []
}

**Read PROJECT_BRIEF.md for full vision and context.**

## Tech Stack

- **Frontend**: Angular + TypeScript + Vite
- **Styling**: Tailwind CSS v4
- **State**: TanStack Query v5 for server state
- **Data (MVP)** localStorage via repository pattern
- **Data (Future)** Supabase (PostgreSQL + Auth)
- **Testing** Vitest + Puppeteer E2E
- **Deployment** Vercel

## Architecture

### Domain-Driven Design Structure

### File Organisation

### Data Models
