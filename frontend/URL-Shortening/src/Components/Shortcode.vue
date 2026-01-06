<template>
  <v-app>
    <!-- App Bar -->
    <v-app-bar class="custom-app-bar">
      <v-btn :to="'/'" class="custom-btn ml-2" style="position: absolute; left: 16px;">back</v-btn>
      <v-app-bar-title class="text-center">
        {{shortCode}}
      </v-app-bar-title>
      <v-btn @click="delDialog = true" class="custom-btn mr-2">delete</v-btn>
    </v-app-bar>

    <!-- Snackbar Component -->
    <v-snackbar
      v-model="messageSB.show"
      :color="messageSB.color"
      :timeout="messageSB.timeout"
      location="bottom"
    >
      {{ messageSB.text }}
    </v-snackbar>

    <!-- delete dialog -->
    <div class="text-center pa-4">
      <v-dialog v-model="delDialog" width="auto">
        <v-card class="text-black" color="#d3d3d3" max-width="400">
          <v-card-title>Are you sure?</v-card-title>
          <v-card-text>{{ shortCode }} will be removed.</v-card-text>
          <v-btn color="#5EC7A1" @click="delDialog = false" text="Cancel"/>
          <v-btn @click="deleteShortUrl" class="text-black" color="#b43e69" text="Delete"/>
        </v-card>
      </v-dialog>
    </div>

    <!-- Main Body -->
    <v-main>

      <!-- displays for shortURL information -->
      <v-list class="mb-4" density="comfortable">
        <v-list-item>ID: {{ id }}</v-list-item>
        <v-list-item>Original URL: {{ longURL }}</v-list-item>
        <v-list-item>Short Code: {{ shortCode }}</v-list-item>
        <v-list-item>Created: {{ createdTime }}</v-list-item>
        <v-list-item>Last Updated: {{ updatedTime }}</v-list-item>
        <v-list-item>Times Accessed: {{ accessedCount }}</v-list-item>
      </v-list>

      <!-- input box for url edit -->
      <v-text-field 
            clearable 
            variant="solo-filled"
            color="primary"
            backgroundColor="white"
            label="Enter the new url..."
            v-model="tempLongURL" 
            placeholder="https://www.newurlexample.com" 
            @keyup.enter="updateUrl"
      />

      <v-btn @click="updateUrl" class="custom-btn">Update</v-btn>

    </v-main>
  </v-app>
</template>

<script>
  import { ref, onMounted } from 'vue'
  import axios from 'axios'
  import { useRouter } from 'vue-router'

  export default {
    props: {
      code: String,
      required: true
    },
    setup(props) {
      const id = ref('')
      const longURL = ref('')
      const shortCode = ref('')
      const updatedTime = ref('')
      const createdTime = ref('')
      const accessedCount = ref('')
      const delDialog = ref(false)
      const tempLongURL = ref('')

      const messageSB = ref({
        show: false,
        text: '',
        color: 'error',
        timeout: 2000
      })

      const router = useRouter()

      async function refresh() {
        try {
          const response = await axios.get(`/api/shorturl/${props.code}`)

          id.value = response.data.id
          longURL.value = response.data.url
          tempLongURL.value = response.data.url
          shortCode.value = response.data.shortCode
          updatedTime.value = response.data.updatedAt
          createdTime.value = response.data.createdAt
          accessedCount.value = response.data.accessCount

        } catch (error) {
          console.error('Error: ', error)
          goHome()
        }
      }

      async function deleteShortUrl() {
        try {
          await axios.delete(`/api/shorturl/${id.value}`)
          showResult('Shortcode deleted successfully', 'success')

          setTimeout(() => {
            goHome()
          }, 2000)

        } catch (error) {
          console.error('Error: ', error)
          showResult('Error: shortCode could not be deleted.')
        }
      }

      async function updateUrl() {
        try {
          const response = await axios.put('/api/shorturl', { url: tempLongURL.value, id: id.value})
          await refresh()
          showResult('URL updated successfully', 'success')

        } catch (error) {
          console.error('Error: ', error)
          showResult('Error: url could not be updated.')
        }
      }

      function goHome() {
        router.push({name: 'Home'})
      }

      function showResult(message, type = 'error') {
        messageSB.value = {
          show: true,
          text: message,
          color: type,
          setTimeout: 3000
        }
      }

      onMounted(refresh)

      return {
        id,
        longURL,
        shortCode,
        updatedTime,
        createdTime,
        accessedCount,
        delDialog,
        tempLongURL,
        messageSB,
        deleteShortUrl,
        updateUrl
      }
    }
  }
</script>

<style>
  .custom-btn {
    background-color: #d3d3d3 !important;
    border-radius: 24px !important;
  }
</style>