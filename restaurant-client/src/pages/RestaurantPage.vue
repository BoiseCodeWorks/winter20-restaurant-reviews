<template>
  <div class="restaurantPage container-fluid">
    <div class="row justify-content-center">
      <div class="col-6 text-center">
        <h3>{{ state.restaurant.name }}</h3>
        <h5>Located at : {{ state.restaurant.location }}</h5>
      </div>
    </div>
    <div class="row">
      <div class="col-4" v-if="state.showForm">
        <h4>Create a Review!</h4>
        <form @submit.prevent="createReview" class="form-group">
          <input type="text" v-model="state.newReview.title" placeholder="Enter a title" required>
          <input type="text" v-model="state.newReview.body" placeholder="Enter a body" required>
          <input type="range" max="5" min="0" v-model="state.newReview.rating" required>
          <input type="checkbox" v-model="state.newReview.published">
          <button type="submit" class="btn btn-info">
            Create Review
          </button>
        </form>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h3>Reviews: <i class="fa fa-plus text-success" @click="state.showForm = !state.showForm" aria-hidden="true"></i></h3>
      </div>
      <review-component v-for="r in state.reviews" :key="r" :review-prop="r" />
    </div>
  </div>
</template>

<script>
import { reactive, onMounted, computed } from 'vue'
import { reviewsService } from '../services/ReviewsService'
import { useRoute } from 'vue-router'
import { restaurantService } from '../services/RestaurantService'
import { AppState } from '../AppState'
export default {
  name: 'RestaurantPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      restaurant: computed(() => AppState.activeRestaurant),
      reviews: computed(() => AppState.reviews),
      showForm: false,
      newReview: {}
    })
    onMounted(() => {
      reviewsService.getRestaurantReviews(route.params.id)
      restaurantService.getRestaurant(route.params.id)
    })
    return {
      state,
      createReview() {
        state.newReview.restaurantId = route.params.id
        reviewsService.createReview(state.newReview)
        state.newReview = {}
        state.showForm = false
      }
    }
  }
}
</script>

<style scoped>

</style>
