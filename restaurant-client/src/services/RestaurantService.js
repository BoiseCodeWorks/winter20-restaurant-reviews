import { AppState } from '../AppState'
import { api } from './AxiosService'

class RestaurantService {
  async getRestaurants() {
    const res = await api.get('api/restaurants')
    console.log(res)
    AppState.restaurants = res.data
  }

  async getRestaurant(id) {
    const res = await api.get('api/restaurants/' + id)
    console.log(res)
    AppState.activeRestaurant = res.data
  }

  async createRestaurant(restData) {
    const res = await api.post('api/restaurants', restData)
    console.log(res)
  }
}

export const restaurantService = new RestaurantService()
