<template>
  <div class="reviewComponent col-3 card">
    <h3>{{ reviewProp.title }}</h3>
    <h5>{{ reviewProp.body }}</h5>
    <p>Rating : {{ reviewProp.rating }}</p>
    <router-link :to="{name: 'Profile', params: {id: reviewProp.owner.id}}">
      <p>Made By: {{ reviewProp.owner.name }}</p>
      <img :src="reviewProp.owner.picture" class="mb-1" alt="">
    </router-link>
  </div>
</template>

<script>
import { reactive } from 'vue'
export default {
  name: 'ReviewComponent',
  props: {
    reviewProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({

    })
    return { state }
  }
}
</script>

<style scoped>

</style>
