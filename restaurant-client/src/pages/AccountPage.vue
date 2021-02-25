<template>
  <div class="about  container-fluid">
    <div class="row justify-content-center">
      <div class="col-6 text-center">
        <h1>Welcome {{ state.account.name }}</h1>
        <img class="rounded" :src="state.account.picture" alt="" />
        <p>{{ state.account.email }}</p>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h4>Create a Restaurant!</h4>
        <form @submit.prevent="createRestaurant" class="form-group">
          <input type="text" v-model="state.newRestaurant.name" placeholder="Enter a name">
          <input type="text" v-model="state.newRestaurant.location" placeholder="Enter a location">
          <button type="submit" class="btn btn-info">
            Create Restaurant
          </button>
        </form>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h3>All My Reviews</h3>
      </div>
      <review-component v-for="r in state.reviews" :key="r" :review-prop="r" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { reviewsService } from '../services/ReviewsService'
import { restaurantService } from '../services/RestaurantService'
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      reviews: computed(() => AppState.reviews),
      newRestaurant: {}
    })
    onMounted(() => reviewsService.getReviewsByAccountId())

    return {
      state,
      createRestaurant() {
        restaurantService.createRestaurant(state.newRestaurant)
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
