console.log("Initilizing");
const Discord = require("discord.js");
const bot = new Discord.Client();
const config = require("./token.json")
const token = config.token;

bot.on("ready", function(){
    console.log("Ready to accept commands, sir! õ7");
});

bot.on("message", message =>{
    if(message.content.toLowerCase().startsWith("!role") && message.channel.name == "bots") {
        var args = message.content.toLowerCase().split(" ");
        console.log(args);
        if(args[1] === "artist") {
            var role = message.guild.roles.find("name", "Artist");
            console.log(message.author + "got the role Artist!");
            message.member.addRole(role.id);
            message.channel.send("Role (Artist) given!");
        }
        else if(args[1] === "animator") {
            var role = message.guild.roles.find("name", "Animator");
            console.log(message.author + "got the role Animator!");
            message.member.addRole(role.id);
            message.channel.send("Role(Animator) given!");
        }
        else if(args[1] === "coder") {
            var role = message.guild.roles.find("name", "Coder");
            console.log(message.author + "got the role Coder!");
            message.member.addRole(role.id);
            message.channel.send("Role (Coder) given!");
        }
        else if(args[1] === "musician") {
            var role = message.guild.roles.find("name", "Musician");
            console.log(message.author + "got the role Musician!");
            message.member.addRole(role.id);
            message.channel.send("Role (Musician) given!");
        }
        else if(args[1] === "modeler") {
            var role = message.guild.roles.find("name", "Modeler");
            console.log(message.author + "got the role Modeler!");
            message.member.addRole(role.id);
            message.channel.send("Role (Modeler) given!");
        }
        else {
            //Reply too user saying that something is wrong
            message.channel.send("No such role found :/");
        }
    }
    else if (message.isMentioned(bot.user)){
        message.reply("! What do you want from me!");
    }
    else {
        console.log("Message was not related to me")
    }
});

bot.login(token);
