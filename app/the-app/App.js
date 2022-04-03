import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import Main from './Pages/Main';
import OpenOrders from './Pages/OpenOrders';
import Order from './Pages/Order';
import Product from './Pages/Product';
import Boxes from './Pages/Boxes';
import SuccessOrder from './Pages/SuccessOrder';

const Stack = createNativeStackNavigator();



function App() {


  return (
    <NavigationContainer >
      <Stack.Navigator screenOptions={{headerShown:false}} initialRouteName="Main">
        <Stack.Screen name="Main" component={Main} />
        <Stack.Screen name="OpenOrders" component={OpenOrders} />
        <Stack.Screen name="Order" component={Order} />
        <Stack.Screen name="Product" component={Product} />
        <Stack.Screen name="Boxes" component={Boxes} />
        <Stack.Screen name="SuccessOrder" component={SuccessOrder} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
export default App;
