-- We need to listen to UDP packets on port 4000

local socket = require "socket"
local udp = assert(socket.udp())
local listenPort = 4000
local sendPort = 4001
local ip = "127.0.0.1"
assert(udp:setsockname(ip, listenPort))
assert(udp:settimeout(0))


-- Listening on port 4000
-- Transmit on port 4001

-- All functions are designed for DCS World
-- Developed by: Michael Saverino -

-- Functions for the DCS World Lua Engine
function setPauseState()
    if DCS.getPause() == false then
        DCS.pause()
        udp:sendto("setPauseState-dcs-server-paused", ip, sendPort)
        TFPLogFunction("setPauseState()", log.DEBUG, "Pausing server")
    else
        DCS.start()
        udp:sendto("setPauseState-dcs-server-resumed", ip, sendPort)
        TFPLogFunction("setPauseState()", log.DEBUG, "Unpausing server")
    end
end

-- Return User Count
function getUserCount()
    local userCount = 0
    for _ in pairs(net.get_player_list()) do userCount = userCount + 1 end
    udp:sendto(userCount, ip, sendPort)
    TFPLogFunction("getUserCount()", log.DEBUG, "User Count: " .. userCount)
end



-- Check if the log file exists
-- If it does, we want to rename it to the current OLDNAME_YYYYMMDD_HHMMSS.log
if lfs.attributes(lfs.writedir() .. "Logs/TFPServerBot.log") then
    -- Get the current time
    local currentTime = os.date("%Y%m%d_%H%M%S")
    -- Rename the file
    os.rename(lfs.writedir() .. "Logs/TFPServerBot.log", lfs.writedir() .. "Logs/TFPServerBot_" .. currentTime .. ".log")
end
-- If it doesn't, we want to create it
if not lfs.attributes(lfs.writedir() .. "Logs/TFPServerBot.log") then
    logFile = io.open(lfs.writedir() .. "Logs/TFPServerBot.log", "w")
    logFile:write("TFPServerBot log file created at " .. os.date("%c"))
    logFile:close()
end

-- Log TFPLogFunction
function TFPLogFunction(category, level, message)
    logFile = io.open(lfs.writedir() .. "Logs/TFPServerBot.log", "a")
    logFile:write("\n" .. os.date("%c") .. " - " .. category .. " - " .. level .. " - " .. message)
    logFile:close()
end

function onSimulationFrame()
    local data, ip, port = udp:receivefrom()
    if data then
        TFPLogFunction("onSimulationLoad", log.DEBUG, "Received: " .. data)
        if data == "dcs-discord-bot-pause" then
            pauseServer()
        end
        if data == "getplayercount" then
            getUserCount()
        end
    end
end

