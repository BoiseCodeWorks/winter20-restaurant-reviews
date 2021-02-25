<template>
  <div class="restaurantComponent card col-3 ">
    <router-link class="text-dark" :to="{name: 'Restaurant', params: {id: restProp.id}}">
      <h4>
        {{ restProp.name }}
      </h4>
      <p>Where are we located ?</p>
      <p>{{ restProp.location }}</p>
    </router-link>
  </div>
</template>

<script>
import { reactive } from 'vue'
export default {
  name: 'RestaurantComponent',
  props: ['restProp'],
  setup(props) {
    const state = reactive({

    })
    return { state }
  }
}
</script>

<style scoped>

</style>
