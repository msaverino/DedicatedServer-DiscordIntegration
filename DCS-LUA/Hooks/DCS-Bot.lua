--[[
    This document is required to be in the hooks folder for the bot to work.
    This will be used to load the bot into the mission.
    This is the only file that is required to be in the hooks folder.
]]

-- What does doFile do?
-- dofile("Scripts/net/DCSServerBot/DCSServerBotMain.lua")
-- dofile("Scripts/net/DCSServerBot/DCSServerBotConfig.lua")
-- dofile("Scripts/net/DCSServerBot/DCSServerBotUtils.lua")
-- dofile("Scripts/net/DCSServerBot/DCSServerBotGameGui.lua")
-- Include our main file
dofile("Scripts/net/TFPServerBot/TFPServerBotMain.lua")
-- Include our log handler
dofile("Scripts/net/TFPServerBot/TFPServerBotLogHandler.lua")
