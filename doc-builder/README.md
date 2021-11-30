# Doc Builder

* [Overview](#overview)
* [Getting Started](#getting-started)
* [Commands](#commands)

## Overview

A .NET 6 / Angular 13 app that accomplishes my higher vision of automating document generation for web-based documents stored in a SQL database. 

## Getting Started

> Coming soon!
>
> Make sure to discuss adjusting the connection string!

## Commands

Some convenience scripts have been added to [`package.json`](./package.json) that enable you to perform the majority of the tasks you need from the base directory of this monorepo.

Command | Description
--------|------------
`build` | Builds the *doc-builder* Angular app
`build:core` | Builds the *core* Angular library embedded in the project
`start` | Starts the *doc-builder* Angular app at `http://localhost:3000`
`watch` | Builds the *core* library in watch mode to prevent subsequent builds
`restore:server` | Restores the server NuGet package dependencies
`start:server` | Builds and runs the server at `http://localhost:5000`
`watch:server` | Builds and starts the server in watch mode at `http://localhost:5000`
`seed` | Applies database migrations and seeds the database by running the [dbseeder](./server/dbseeder/Program.cs) utility, which executes the [DbInitailizer.Initialize](./server/DocBuilder.Data/Extensions/DbInitailizer.cs#L7) method.
`update:db` | Applies database migrations to the target database