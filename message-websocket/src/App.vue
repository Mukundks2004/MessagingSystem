<template>
  <MessageTable></MessageTable>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import MessageTable from './components/MessageTable.vue'

export default defineComponent({
  name: 'App',
  components: {
    MessageTable
  }
});
</script>

<style>
</style>
