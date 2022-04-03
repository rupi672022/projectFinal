import { View, Text, ImageBackground, TouchableOpacity, StyleSheet, Image } from 'react-native'
import React from 'react'
import imagelogo from '../Images/yakobs.png';
import image from '../Images/logo.jpg';

export default function SuccessOrder(props) {

  //date
  var today = new Date(),
    date = today.getDate() + '/0' + (today.getMonth() + 1) + '/' + today.getFullYear();
  const currentDate = date;

  const workerNum= props.route.params.workerNum ;;//num of worker

  const apiUrlOrder = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Orders?preparationDate=";


  const btnNewOrder = () => {//get the orders to this worker
    fetch(apiUrlOrder +currentDate+ "&id=" + workerNum, {
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
          console.log(result)
          { props.navigation.navigate('OpenOrders', { orderArr:result,workerNum:workerNum }) };
        },
        (error) => {
          console.log("err post=", error);
        });
  }


  return (
    <View style={styles.view}>
      <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
        <Text style={styles.logo}>משק יעקבס</Text>
        <Text style={styles.dateText}>{currentDate}</Text>
        <Text style={styles.headText}> הזמנה : {props.route.params.num}</Text>
      </ImageBackground>

      <Text style={styles.text}>
        הזמנה {props.route.params.num} נקלטה בהצלחה
      </Text>

      <TouchableOpacity onPress={btnNewOrder}>
        <Text
          style={styles.buttonText} >המשך </Text>
      </TouchableOpacity>

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
    fontSize: 30,
    color: 'black',
    marginTop: 20,
    marginBottom: 10,
    marginLeft: 30,
    width: 330,
    textAlign: 'center',
    backgroundColor: '#FFE587'

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
    marginLeft: 70,
    borderColor: '#FFE587',
  },
  buttonText: {
    fontSize: 30,
    borderColor: '#FFE587',
    color: 'black',
    borderWidth: 2,
    borderRadius: 25,
    marginLeft: 100,
    marginTop: 30,
    marginRight: 80,
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
  yakobsimg: {
    marginTop: 190,
    marginLeft: 10,
    marginBottom: 20,
    width: 70,
    height: 70
  }

});