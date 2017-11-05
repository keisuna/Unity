var HMD;
var frameData;

function getVRDevices() {
    if (navigator.getVRDisplays) {
        navigator.getVRDisplays().then(function (devices) {
            for (var i = 0; i < devices.length; i++) {
                if (devices[i] != null) {
                    HMD = devices[i];
                    console.log("get HMD device");
                    console.log(HMD.displayName);
                    var button = document.getElementById('button');
                    button.disabled = false;
                    break;
                }
            }
        });
    }
}
function webvr_click() {
    console.log("change mode");
    if (!HMD) {
        alert("Can't find HMD display");
        return;
    }
    fullScreen();
    gameInstance.SendMessage('CameraSet', 'ChangeMode', 'vr');
}
function fullScreen() {
    frameData = new VRFrameData();
    myCanvas = document.getElementById('#canvas');
    console.log(HMD);
    HMD.requestPresent([{ source: myCanvas }]).then(function () {
        console.log('Presenting to WebVR display');

        // Set the canvas size to the size of the vrDisplay viewport

        var leftEye = HMD.getEyeParameters('left');
        var rightEye = HMD.getEyeParameters('right');

        myCanvas.width = Math.max(leftEye.renderWidth, rightEye.renderWidth) * 2;
        myCanvas.height = Math.max(leftEye.renderHeight, rightEye.renderHeight);

        getVRSensorState();
    });
    if (myCanvas.requestFullscreen)
    {
        myCanvas.requestFullscreen({ vrDisplay: HMD });
    } else if (myCanvas.msRequestFullscreen)
    {
        myCanvas.msRequestFullscreen({ vrDisplay: HMD });
    } else if (myCanvas.mozRequestFullScreen)
    {
        myCanvas.mozRequestFullScreen({ vrDisplay: HMD });
    } else if (myCanvas.webkitRequestFullscreen)
    {
        myCanvas.webkitRequestFullscreen({ vrDisplay: HMD });
    }
}
function getVRSensorState() {

    HMD.requestAnimationFrame(getVRSensorState);
    HMD.getFrameData(frameData);
    var curFramePose = frameData.pose;
    if (curFramePose != null) {
        var orientation = curFramePose.orientation;
        gameInstance.SendMessage('CameraSet', 'RotationX', -orientation[0]);
        gameInstance.SendMessage('CameraSet', 'RotationY', -orientation[1]);
        gameInstance.SendMessage('CameraSet', 'RotationZ', orientation[2]);
        gameInstance.SendMessage('CameraSet', 'RotationW', orientation[3]);

        var position = curFramePose.position;
        gameInstance.SendMessage('CameraSet', 'PositionX', position[0]);
        gameInstance.SendMessage('CameraSet', 'PositionY', position[1]);
        gameInstance.SendMessage('CameraSet', 'PositionZ', position[2]);
    }

}
function Render() {
    HMD.submitFrame();
}