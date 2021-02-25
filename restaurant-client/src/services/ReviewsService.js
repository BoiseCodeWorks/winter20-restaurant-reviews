import { AppState } from '../AppState'
import { api } from './AxiosService'

class ReviewsService {
  async getRestaurantReviews(id) {
    const res = await api.get('api/restaurants/' + id + '/reviews')
    console.log(res)
    AppState.reviews = res.data
  }

  async getReviewsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/reviews')
    console.log(res)
    AppState.reviews = res.data
  }

  async getReviewsByAccountId() {
    const res = await api.get('account/reviews')
    console.log(res)
    AppState.reviews = res.data
  }

  async createReview(reviewData) {
    await api.post('api/reviews', reviewData)
    this.getRestaurantReviews(reviewData.restaurantId)
  }
}

export const reviewsService = new ReviewsService()
