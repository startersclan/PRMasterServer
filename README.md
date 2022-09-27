# PRMasterServer

[![github-actions](https://github.com/startersclan/prmasterserver/workflows/ci-master-pr/badge.svg)](https://github.com/startersclan/prmasterserver/actions)
[![github-release](https://img.shields.io/github/v/release/startersclan/prmasterserver?style=flat-square)](https://github.com/startersclan/prmasterserver/releases/)
[![docker-image-size](https://img.shields.io/docker/image-size/startersclan/prmasterserver/latest)](https://hub.docker.com/r/startersclan/prmasterserver)

A GameSpy replacement Master Server for [Project Reality: BF2](http://www.realitymod.com). This emulates the GameSpy API in order to keep PR:BF2 playable after the Battlefield 2 GameSpy shutdown.

## Features

Supports **Battlefield 2**'s GameSpy implementation. No other games are supported (yet?). If you wish to modify the code and add support for your game, or add any additional features, please feel free to make a **fork** and submit a [pull request](https://help.github.com/articles/using-pull-requests).

- Login Server (Uses SQLite for the database)
    - Creating Accounts
    - Retrieving accounts by username/email (allows multiple accounts per email)
    - Log in
- Server Browser
    - Server Reporting (Game Server registering with Master Server)
    - Server Retrieval (Client requesting a server list)
    - Supports filters
    - GeoIP
- CD Key Authentication
    - Accepts all CD Keys with no further checks.

## Usage

The docker image includes the binary, dependencies, and the [MaxMind GeoIP2 Country](https://www.maxmind.com/en/country) database.

```sh
# Test run
docker run --rm -it -p 29900:29900/tcp -p 29901:29901/tcp -p 28910:28910/tcp -p 27900:27900/udp -p 29910:29910/udp startersclan/prmasterserver:latest

# Run with support for bf2 and fh2
printf "bf2\nfh2\n" > modwhitelist.txt
docker volume create data-volume
docker run --rm -it -p 29900:29900/tcp -p 29901:29901/tcp -p 28910:28910/tcp -p 27900:27900/udp -p 29910:29910/udp \
    -v modwhitelist.txt:/app/modwhitelist.txt:ro \
    -v data-volume:/data \
    startersclan/prmasterserver:latest

# Now launch Battlefield 2, and try creating a multiplayer account and logging in and out
```

## Setting up the project

1. Be sure to have [Visual Studio 2013](http://www.microsoft.com/en-us/download/details.aspx?id=40787) installed.  You might be able to compile it using previous versions of Visual Studio or using Mono, but this is untested and may not work.

2. Open **PRMasterServer.sln**, and build. This should download via NuGet any extra packages required.

3. Grab the latest [MaxMind GeoIP2 Country](https://www.maxmind.com/en/country) database, or use the free [GeoLite2 Country](http://dev.maxmind.com/geoip/geoip2/geolite2/) database. Put it in the same folder as **PRMasterServer.exe**.

4. Create a **modwhitelist.txt** file containing line separated mod names (i.e. bf2, pr, fh2) to allow servers running these mods to register with the master server. Or, just use **%** to allow all mods. If you don't have a **modwhitelist.txt** file, it will default to Project Reality: BF2 mod names (*pr* and *pr!_%*).
> **Tip:** % is wildcard, _ is placeholder, ! is escape, # at the start of the line is a comment, empty lines are ignored.

5. Run **PRMasterServer.exe +db LoginDatabase.db3** and it should start up with no errors. You can use an optional **+bind xxx.xxx.xxx.xxx** paramter to bind the server to a specific network interface, or by default it will bind to all available interfaces.

6. If there's issues, unlucky, I'm sure you'll be able to figure them out :).

## Stuff to do

Of course, no project is ever really *complete*, there's plenty of other stuff that could be done. Maybe in the future it just might happen.

- Comment the code so you poor folk can understand the black magic.
- Manage account protocol (delete accounts, change password, change email).
- Maybe support some other games than just Battlefield 2. But isn't that the point of open sourcing and putting it on GitHub? If you  want it, make a fork and do it ;).

## Credits

[Luigi Auriemma](http://aluigi.org) for reverse engineering the GameSpy protocol and encryption.
