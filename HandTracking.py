## py -3.10 -u "d:\Progs\Py\CV\HandTracking.py"

import cv2 as cv
import cvzone.HandTrackingModule as htm
import socket
import json
import time


WIDTH, HEIGHT = 640, 480 # 1280,720
ADRESS_PORT = ("127.0.0.1", 5052)


cam = cv.VideoCapture(0)
detector = htm.HandDetector(detectionCon=0.8, maxHands=1)
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # UDP
sock.connect(ADRESS_PORT)

cam.set(3, 640)
cam.set(4, 480) 



while True:
    try:
        _, frame = cam.read()
        if frame is None:
            raise Exception("No camera frame available")  
        hands, img = detector.findHands(frame)

        # output - Landmarks (x,y,z) , total no 21.
        if hands:
            hand = hands[0]
            lmList = hand["lmList"]

            processed = [ [x,100-y,z] for x,y,z in lmList]
            
            print(len(processed), end='\r')
            data = json.dumps(processed).encode('utf-8')
            sock.sendto(data, ADRESS_PORT)
        time.sleep(0.05) # = 1/FPS

    except Exception as e:
        print("Camera or socket error:", e)
        continue  
        #print(hand)
    
    cv.imshow("Camera Feed", cv.resize(img, (WIDTH, HEIGHT),0.5,0.5))
    cv.waitKey(1)