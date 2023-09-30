var express=require('express');
var app=express();
var server=require('http').createServer(app);
var io=require("socket.io").listen(server);
var colors=require('colors');
var leftOrright=1;


io.on('connection',function(socket){
socket.on("indicatevector",function(){
    if(leftOrright===1){
        leftOrright=-1
    }else{
        leftOrright=1;
    }
    
     var client={
        vectorofclient:leftOrright
    }
         socket.emit("sendproperty",{point:client.vectorofclient});
    socket.broadcast.emit("OTHERPLAY",{point:client.vectorofclient});
});
    socket.on("movetoleft",function(){
        socket.broadcast.emit("movetoleft");
    });
        socket.on("movetoright",function(){
        socket.broadcast.emit("movetoright");
    });
        socket.on("stop",function(){
        socket.broadcast.emit("stop");
    });
    socket.on("stright",function(){
        socket.broadcast.emit("stright");
    });
    socket.on("jump",function(){
       socket.broadcast.emit("jump"); 
    });
    socket.on("chip",function(){
        socket.broadcast.emit("chip");
    });
    socket.on("hide",function(){
        socket.broadcast.emit("hide");
    });
        socket.on("hideplayer",function(){
        socket.broadcast.emit("hideplayer");
    });
});

app.set('port',process.env.PORT || 3000);
server.listen(app.get('port'),function(){
    console.log("welcome".yellow);
    setTimeout(function(){
        console.log("server is online".green);
    },1000);
});