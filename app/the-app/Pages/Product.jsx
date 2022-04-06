import React, { useState, useEffect } from 'react';
import { View, Text, ImageBackground, TouchableOpacity, Alert } from 'react-native';
import { StyleSheet, TextInput, Image } from "react-native";
import { Icon, Overlay } from 'react-native-elements';
import { BarCodeScanner } from 'expo-barcode-scanner';
import image from '../Images/logo.jpg';
import imagelogo from '../Images/yakobs.png';

export default function Product(props) {

    //date
    var today = new Date(),
        date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
    const currentDate = date;


    const product = props.route.params.product;//info about the product
    const workerNum= props.route.params.workerNum ;

    const apiUrl = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Products";

    const [weight, setWeight] = useState(0);//weight on text input
    const [weightArr, setWeightArr] = useState([]);//arr with weight
    const [total, setTotal] = useState(0);//total to this product

    //Barcode
    const [hasPermission, setHasPermission] = useState(null);
    const [scanned, setScanned] = useState(false);
    const [data, setData] = useState(0);


    //Overlay
    const [visible, setVisible] = useState(false);
    const toggleOverlay = () => {
        setVisible(!visible);
    };

    const [visible1, setVisible1] = useState(false);
    const toggleOverlay1 = () => {
        setVisible1(!visible1);
    };

    const [visible2, setVisible2] = useState(false);
    const toggleOverlay2 = () => {
        setVisible2(!visible2);
    };


    const add = () => {//add to arr weight
        if (parseFloat(total) + parseFloat(weight) < product.WeightAll + 0.5) {
            let newObj = [...weightArr, weight - product.DownfromTotal];
            setWeightArr(newObj);
            setWeight(0);
        }
        toggleOverlay();
    }

    useEffect(() => {//change the total
        let sum = 0;
        weightArr.map(obj => {
            let x = parseFloat(obj).toFixed(2);
            sum = parseFloat(sum) + parseFloat(x);
            setTotal(sum);
        })

    }, [weightArr])

    const check = () => {//check the total 
        let ans = product.WeightAll - total;
        if (ans <= 0.5) {
            updateProduct()
        }
    }

    //Barcode
    useEffect(() => {
        (async () => {
            const { status } = await BarCodeScanner.requestPermissionsAsync();
            setHasPermission(status === 'granted');
        })();
    }, []);

    const handleBarCodeScanned = ({ type, data }) => {
        setData(data);
        setScanned(true);
        toggleOverlay2()
        if ('7290106291089' == data) {
            Alert.alert(
                "המוצר נסרק בהצלחה !"
            );
        }
    };

    if (hasPermission === null) {
        return <Text>אפשר להתחבר עם מצלמה</Text>;
    }
    if (hasPermission === false) {
        return <Text>אין גישה למצלמה</Text>;
    }

    const updateProduct = () => {

        let updateProduct = [{ Barcod: product.Barcod, WeightAll: total, Quantity: props.route.params.num }];
        updateProduct = updateProduct[0];
        fetch(apiUrl, {
            method: 'PUT',
            body: JSON.stringify(updateProduct),
            headers: new Headers({
                'Content-Type': 'application/json ; charset=UTP-8',
                'Accept': 'application/json ; charset=UTP-8'
            })
        })
            .then(res => {
                { props.navigation.navigate('Order', { num: props.route.params.num, arr: props.route.params.arr,workerNum:workerNum  }) };

            }).catch(error => {
                console.log('error=', error)
            })

    }


    return (
        <View style={styles.view}>
            <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
                <Text style={styles.logo}>משק יעקבס</Text>
                <Text style={styles.dateText}>{currentDate}</Text>
                <Text style={styles.headText}> הזמנה : {props.route.params.num}</Text>
            </ImageBackground>

            <Text style={styles.text}> </Text>

            <View style={{ flexDirection: 'row', borderWidth: 1, margin: 10 }}>
                <View>
                    <Text style={styles.textBarcode}>סרוק את המוצר :</Text>
                    <TouchableOpacity onPress={toggleOverlay2} >
                        <Icon
                            name='check-circle'
                            size={30}
                            color={'7290106291089' == data ? 'green' : '#efefef'}
                            style={{ marginTop: 10, marginLeft: 50 }}
                        />
                    </TouchableOpacity>

                    <Overlay isVisible={visible2} onBackdropPress={toggleOverlay2}>
                        <View style={styles.container}>
                            <BarCodeScanner
                                onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
                                style={StyleSheet.absoluteFillObject}
                            />
                            {scanned &&
                                <TouchableOpacity onPress={() => setScanned(false)}>
                                    <Text style={styles.textBarcodeAgien}>לחץ כדי לסרוק שוב</Text>
                                </TouchableOpacity>}
                        </View>
                        <TouchableOpacity onPress={toggleOverlay2}>
                            <Text style={styles.buttonOvarlay2}>סגור</Text>
                        </TouchableOpacity>
                    </Overlay>


                    <TouchableOpacity onPress={toggleOverlay1} disabled={product.Quantity == '0' ? false : true} >
                        <Text style={{
                            marginLeft: 45, marginTop: 30, fontSize: 15,
                            borderColor: product.Quantity == '0' ? '#FFE587' : '#ffff',
                            color: product.Quantity == '0' ? 'black' : '#ffff',
                            borderWidth: 2,
                            backgroundColor: product.Quantity == '0' ? '#FFE587' : '#ffff',
                            padding: 3,
                            textAlign: 'center',
                            borderRadius: 20,
                        }}>משקלי המוצר</Text>
                    </TouchableOpacity>

                    <Overlay isVisible={visible1} onBackdropPress={toggleOverlay1}>
                        {weightArr.map((item) => {
                            return (
                                <View>
                                    <Text style={{ textAlign: 'center', padding: 3 }}>{item + ' ק"ג'}</Text>
                                </View>
                            )
                        })}
                        <TouchableOpacity onPress={toggleOverlay1}>
                            <Text style={styles.buttonOvarlay1}> סגור</Text>
                        </TouchableOpacity>
                    </Overlay>
                </View>



                <View>
                    <View>
                        <Text style={styles.listTaxt}>שם המוצר : {product.NameProduct}</Text>
                        <Text style={styles.listTaxt}>ברקוד : {product.Barcod}</Text>
                        <Text style={styles.listTaxt}>סוג מוצר : {product.Type}</Text>
                        <Text style={styles.listTaxt}>{product.Quantity == '0' ? 'משקל כולל : ' + product.WeightAll : 'כמות : ' + product.Quantity}</Text>

                        <View>
                            <TouchableOpacity disabled={product.Quantity == '0' ? false : true} onPress={toggleOverlay}>
                                <Text style={{
                                    fontSize: 15,
                                    marginLeft: 150,
                                    marginRight: 10,
                                    padding: 3,
                                    textAlign: 'center',
                                    borderRadius: 20,
                                    borderColor: product.Quantity == '0' ? '#FFE587' : '#ffff',
                                    color: product.Quantity == '0' ? 'black' : '#ffff',
                                    borderWidth: 2,
                                    backgroundColor: product.Quantity == '0' ? '#FFE587' : '#ffff',
                                }}>הכנס משקל</Text>
                            </TouchableOpacity>

                            <Overlay isVisible={visible} onBackdropPress={toggleOverlay}>
                                <Text style={{ textAlign: 'center', fontSize: 18 }}>הכנס את משקל המוצר (בק"ג) : </Text>
                                <TextInput
                                    keyboardType='numeric'
                                    style={styles.inputText}
                                    onChangeText={newText => setWeight(newText)}
                                />
                                <TouchableOpacity onPress={add}>
                                    <Text style={styles.buttonOvarlay}>שמור</Text>
                                </TouchableOpacity>
                            </Overlay>
                        </View>

                        <Text style={styles.listTaxt}>סך הכול : {product.Quantity == '0' ? total + ' ק"ג' : '---'}
                        </Text>
                    </View >
                </View >
            </View>

            <TouchableOpacity onPress={check}>
                <Text style={styles.buttonText} >שמור</Text>
            </TouchableOpacity>

            <Image source={imagelogo} style={styles.yakobsimg}></Image>
        </View >

    )
}

const styles = StyleSheet.create({
    view: {
        backgroundColor: 'white',
        height: '100%',
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
        marginTop: -90,
        marginBottom: 10,

    },
    listTaxt: {
        fontSize: 20,
        padding: 10,
        textAlign: 'right',
        marginLeft: -50
    },
    inputText: {
        alignItems: 'center',
        backgroundColor: "#ffeba1",
        width: 200, height: 40,
        marginTop: 10,
        textAlign: 'center'
    },
    buttonText: {
        fontSize: 30,
        borderColor: '#FFE587',
        color: 'black',
        borderWidth: 2,
        borderRadius: 20,
        marginLeft: 180,
        marginRight: 20,
        padding: 10,
        textAlign: 'center'
    },
    buttonOvarlay: {
        fontSize: 20,
        marginTop: 10,
        textAlign: 'center',
        borderWidth: 2,
        backgroundColor: '#FFE587',
        width: 50,
        marginLeft: 70,
        borderColor: '#FFE587',
    },
    buttonOvarlay1: {
        fontSize: 20,
        marginTop: 10,
        textAlign: 'center',
        borderWidth: 2,
        backgroundColor: '#FFE587',
        width: 50,
        borderColor: '#FFE587',
    },
    buttonOvarlay2: {
        fontSize: 20,
        marginTop: 10,
        textAlign: 'center',
        borderWidth: 2,
        backgroundColor: '#FFE587',
        width: 50,
        borderColor: '#FFE587',
        marginLeft: 80
    },
    yakobsimg: {
        marginTop: 50,
        marginLeft: 10,
        marginBottom: 20,
        width: 70,
        height: 70
    },
    container: {
        height: 310,
        width: 200
    },
    textBarcode: {
        marginLeft: 45,
        marginTop: 50,
        fontSize: 15,
        color: 'black',
        padding: 3,
        textAlign: 'center',
    },
    textBarcodeAgien: {
        color: '#fff',
        fontSize: 15,
        textAlign: 'center',
        backgroundColor: 'black',
        width: 120,
        marginLeft: 40,
        marginTop: 280,
    }
});
