<template>
  <div class="profilePage container-fluid">
    <div class="row mt-3">
      <div class="col text-center">
        <img :src="state.profile.picture" alt="">
        <h2>
          Welcome To {{ state.profile.name }}'s Page
        </h2>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h4>All my reviews:</h4>
      </div>
      <review-component v-for="r in state.reviews" :key="r" :review-prop="r" />
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { useRoute } from 'vue-router'
import { profileService } from '../services/ProfileService'
import { AppState } from '../AppState'
import { reviewsService } from '../services/ReviewsService'
export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.searchedProfile),
      reviews: computed(() => AppState.reviews)
    })
    onMounted(() => {
      profileService.getProfileById(route.params.id)
      reviewsService.getReviewsByProfileId(route.params.id)
    })
    return { state }
  }
}
</script>

<style scoped>

</style>
