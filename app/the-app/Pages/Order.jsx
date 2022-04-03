import React, { useEffect, useState } from 'react';
import { View, Text, ImageBackground, TouchableOpacity, FlatList, StyleSheet, Image } from 'react-native';
import { Overlay } from 'react-native-elements';
import image from '../Images/logo.jpg';
import imagelogo from '../Images/yakobs.png';

export default function Order(props) {

  //date
  var today = new Date(),
    date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
  const currentDate = date;

  const [arrProducts, setArrProducts] = useState([]);//arr of products

  const workerNum= props.route.params.workerNum ;

  //Overlay
  const [visible, setVisible] = useState(false);
  const toggleOverlay = () => {
    setVisible(!visible);
  }

  const apiUrlProduct = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Products?id";

  useEffect(() => {
    let arr = props.route.params.arr;
    arr.map(num =>//get the info about the product
      fetch(apiUrlProduct + "=" + num.OrderNum, {
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
            setArrProducts(result)
          },
          (error) => {
            console.log("err post=", error);
          })
    );

  }, [props.route.params])


  const btnOrder = () => {//finish all the products
    let arr = arrProducts.filter(res => res.Status == '1');
    if (arr.length == 0) { props.navigation.navigate('Boxes', { num: props.route.params.num, workerNum: workerNum }) }
    else toggleOverlay();
  }

  return (
    <View style={styles.view}>
      <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
        <Text style={styles.logo}>משק יעקבס</Text>
        <Text style={styles.dateText}>{currentDate}</Text>
        <Text style={styles.headText}> הזמנה : {props.route.params.num}</Text>
      </ImageBackground>

      <Text style={styles.text}> </Text>

      <FlatList
        data={arrProducts}
        renderItem={({ item }) => (
          <View key={item.ProductNum}>
            <TouchableOpacity style={styles.tuchtext} disabled={item.Status == '1' ? false : true}
              onPress={() =>
                props.navigation.navigate('Product', { num: props.route.params.num, product: item, arr: props.route.params.arr,workerNum:workerNum })}>
              <Text style={{
                fontSize: 20,
                padding: 5,
                borderWidth: 1,
                marginLeft: 170,
                width: '50%',
                borderColor: item.Status == '1' ? '#e4e4e4' : '#98FB98',
                textAlign: 'center',
                backgroundColor: item.Status == '1' ? '#e4e4e4' : '#98FB98'
              }}>{item.NameProduct} </Text>
            </TouchableOpacity>
          </View>

        )}
      />


      <View>
        <TouchableOpacity onPress={btnOrder}>
          <Text style={styles.buttonText} >שמור</Text>
        </TouchableOpacity>

        <Overlay isVisible={visible} onBackdropPress={toggleOverlay}>
          <Text style={{ textAlign: 'center', fontSize: 18 }}>לא סיימת את ההזמנה, אנא המשך</Text>
          <TouchableOpacity onPress={toggleOverlay}>
            <Text style={styles.buttonOvarlay}>אישור</Text>
          </TouchableOpacity>
        </Overlay>
      </View>


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

  },
  tuchtext: {
    padding: 5,
  },
  listText: {
    fontSize: 20,
    padding: 5,
    borderWidth: 1,
    marginLeft: 170,
    width: '50%',
    borderColor: '#e4e4e4',
    backgroundColor: '#e4e4e4',
    textAlign: 'center',

  },
  buttonText: {
    fontSize: 30,
    borderColor: '#FFE587',
    color: 'black',
    borderWidth: 2,
    borderRadius: 20,
    marginLeft: 180,
    marginTop: -100,
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
  yakobsimg: {
    marginTop: 50,
    marginLeft: 10,
    marginBottom: 20,
    width: 70,
    height: 70
  },
  textOverly:{
    textAlign: 'center',
     fontSize: 30
  }

});