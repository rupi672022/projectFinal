import React, { useState, useEffect } from 'react';
import { View, Text, ImageBackground, TextInput, TouchableOpacity, StyleSheet, Image } from 'react-native';
import { Overlay, Icon } from 'react-native-elements';
import { Camera } from 'expo-camera';
import image from '../Images/logo.jpg';
import imagelogo from '../Images/yakobs.png';

export default function Boxes(props) {

    //date
    var today = new Date(),
        date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
    const currentDate = date;

    const [boxesNum, setBoxesNum] = useState(0);//box to this order

    const [hasPermission, setHasPermission] = useState(null);
    const [type, setType] = useState(Camera.Constants.Type.back);
    const [camera, setCamera] = useState(null);


    const [picUri, setPicUri] = useState();//camera

    const apiUrlPut = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Orders";

    const apiUrlGet = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Orders?idOrder=";

    const workerNum= props.route.params.workerNum ;



    //Overlay
    const [visible, setVisible] = useState(false);
    const toggleOverlay = () => {
        setVisible(!visible);
    }

    const [visible1, setVisible1] = useState(false);
    const toggleOverlay1 = () => {
        setVisible1(!visible1);
    };

    //camera
    useEffect(() => {
        (async () => {
            const { status } = await Camera.requestCameraPermissionsAsync();
            setHasPermission(status === 'granted');
        })();
    }, []);

    if (hasPermission === null) {
        return <View />;
    }
    if (hasPermission === false) {
        return <Text>אין גישה למצלמה</Text>;
    }


    const btnBoxes = () => {//update the image in server
        if (boxesNum != '0' && picUri!=undefined) {
            let updateOrder = [{ OrderNum: props.route.params.num, Image: picUri }];
            updateOrder = updateOrder[0];
            fetch(apiUrlPut, {
                method: 'PUT',
                body: JSON.stringify(updateOrder),
                headers: new Headers({
                    'Content-Type': 'application/json ; charset=UTP-8',
                    'Accept': 'application/json ; charset=UTP-8'
                })
            })
                .then(res => {
                    console.log("ok")
                    getImage();
                }).catch(error => {
                    console.log('error=', error)
                })
        }
        else toggleOverlay();

    }


    const getImage = () => {//get the image from the server
        fetch(apiUrlGet + props.route.params.num, {
            method: 'GET',
            headers: new Headers({
                'Content-Type': 'application/json ; charset=UTP-8',
                'Accept': 'application/json ; charset=UTP-8'
            })
        })
            .then(res => {
                return res.json();
            })
            .then(
                (result) => {
                    setPicUri(result[0].Image);
                    btnNext();
                },
                (error) => {
                    console.log("err post=", error);
                });
    }


    const btnNext = () => {//finish the order
        {props.navigation.navigate('SuccessOrder', { num: props.route.params.num, workerNum: workerNum }) }
    }

    return (
        <View style={styles.view}>
            <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
                <Text style={styles.logo}>משק יעקבס</Text>
                <Text style={styles.dateText}>{currentDate}</Text>
                <Text style={styles.headText}> הזמנה : {props.route.params.num}</Text>
            </ImageBackground>

            <Text style={styles.text}> כמות ארגזים בהזמנה זו : </Text>

            <TextInput
                style={styles.inputText}
                number={boxesNum}
                onChangeText={setBoxesNum}
            />

            <View>
                <TouchableOpacity onPress={toggleOverlay1} style={styles.butoonCamera}>
                    <Text style={{ fontSize: 20, textAlign: 'center' }}>צלם את המשטח</Text>
                </TouchableOpacity>
                <Overlay isVisible={visible1} onBackdropPress={toggleOverlay1}>
                    <View style={styles.container}>
                        <Camera style={styles.camera} ref={ref => setCamera(ref)} type={type}>
                            <View style={styles.buttonContainer}>
                                <TouchableOpacity
                                    style={styles.button}
                                    onPress={() => {
                                        setType(
                                            type === Camera.Constants.Type.back
                                                ? Camera.Constants.Type.front
                                                : Camera.Constants.Type.back
                                        );
                                    }}>
                                    <Icon name='sync' color='white' />
                                </TouchableOpacity>
                            </View>
                        </Camera>
                    </View>
                    <TouchableOpacity onPress={async () => {
                        if (camera) {
                            const data = await camera.takePictureAsync(null);
                            console.log(data.uri)
                            setPicUri(data.uri);
                        }
                        toggleOverlay1();
                    }}>
                        <Icon name='circle' size={40} />
                    </TouchableOpacity>
                </Overlay>
            </View>


            <View>
                <TouchableOpacity onPress={btnBoxes}>
                    <Text style={styles.buttonText} >
                        שמור</Text>
                </TouchableOpacity>

                <Overlay isVisible={visible} onBackdropPress={toggleOverlay}>
                    <Icon name='warning' color='#ff4500' size={30} />

                    <Text style={{ textAlign: 'center', fontSize: 18 }}>אנא הכנס כמות ארגזים או צלם את המשטח
                    </Text>
                    <TouchableOpacity onPress={toggleOverlay}>
                        <Text style={styles.buttonOvarlay}>
                            אישור
                        </Text>
                    </TouchableOpacity>
                </Overlay>
            </View>


            <Image source={{ uri: picUri }} style={{ width: 150, height: 150, marginLeft: 120, marginTop: 35 }} />

            <Image source={imagelogo} style={styles.yakobsimg}></Image>

        </View>
    )
}


const styles = StyleSheet.create({
    view: {
        backgroundColor: 'white',
        height: '100%'
    },
    imageback: {
        width: '140%',
        height: 400
    },
    logo: {
        fontSize: 15,
        color: 'black',
        fontWeight: 'bold',
        marginLeft: 290,
        marginTop: 50,
    },
    dateText: {
        fontSize: 15,
        color: 'black',
        marginLeft: 290,
    },
    headText: {
        fontSize: 50,
        fontWeight: 'bold',
        color: 'black',
        marginLeft: 50,
        marginTop: 30
    },
    text: {
        fontSize: 20,
        color: 'black',
        marginTop: -100,
        marginBottom: 10,
        marginLeft: 150,

    },
    inputText: {
        marginLeft: 130,
        width: 200,
        height: 40,
        marginTop: 10,
        backgroundColor: '#FFE587',
        textAlign: 'center'
    },
    buttonOvarlay: {
        fontSize: 20,
        marginTop: 10,
        textAlign: 'center',
        borderWidth: 2,
        backgroundColor: '#FFE587',
        width: 50,
        marginLeft: 120,
        borderColor: '#FFE587',
    },
    buttonText: {
        fontSize: 30,
        borderColor: '#FFE587',
        color: 'black',
        borderWidth: 2,
        borderRadius: 25,
        marginLeft: 130,
        marginTop: 30,
        marginRight: 40,
        padding: 10,
        textAlign: 'center'
    },
    yakobsimg: {
        marginLeft: 10,
        marginBottom: 20,
        width: 70,
        height: 70
    },
    container: {
        height: 310,
    },
    camera: {
        width: 250,
        height: 300
    },
    buttonContainer: {
        flex: 1,
        backgroundColor: 'transparent',
        flexDirection: 'row',
        margin: 20,
    },
    button: {
        flex: 1,
        alignSelf: 'flex-end',
        alignItems: 'center',
    },
    textCamera: {
        fontSize: 18,
        color: 'white',
    },
    butoonCamera:{
        marginLeft: 130, marginTop: 50, marginRight: 50,
        color: 'black',
        padding: 2,
        textAlign: 'center',
        borderColor: '#FFE587',
        borderWidth: 2,
    }

});