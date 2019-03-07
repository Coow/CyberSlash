console.log("Initilizing");
const Discord = require("discord.js");
const bot = new Discord.Client();
const config = require("./token.json")
const token = config.token;

bot.on("ready", () => {
    console.log("Ready to accept commands, sir! õ7");
});

bot.on("message", message =>{
    if(message.content.toLowerCase().startsWith("!role") && message.channel.name == "bots") {
        const args = message.content.toLowerCase().split(" ");
        console.log(args);
        if (!args[1] || !["artist", "animator", "coder", "musician", "modeler"].includes(args[1].toLowerCase())) {
            return message.channel.send("No such role found :/");
        }
        const roles = {
            artist: "Artist",
            animator: "Animator",
            coder: "Coder",
            musician: "Musician",
            modeler: "Modeler"
        }
        const role = message.guild.roles.find(r => r.name === roles[args[1].toLowerCase()])
        if (message.member.roles.has(role.id)) {
            return message.channel.send(`You already got the ${roles[args[1].toLowerCase()]} Role`)
        }
        message.member.addRole(role.id)
        return message.channel.send(`Role (${roles[args[1].toLowerCase()]}) given!`)
    }
    else if (message.isMentioned(bot.user)){
        message.reply("! What do you want from me!");
    }
    else {
        console.log("Message was not related to me")
    }
});

bot.login(token);
