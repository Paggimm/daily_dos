export async function fetchWithTimeout(
    resource: RequestInfo,
    options: RequestInit,
    timeout: number
): Promise<Response>
{
    const controller = new AbortController()
    const id = setTimeout(() => controller.abort(), timeout)

    const response = await fetch(resource, {
        ...options,
        signal: controller.signal,
    })
    clearTimeout(id)

    return response
}

/**
 * wrapper method to send a default request
 * @param uri endpoint without base-url
 * @param body json-encoded string or FormData
 * @param httpMethod GET|POST|PATCH|DELETE
 * @param token authorization-token
 */
export async function fetchRequest(uri: string, body: string | FormData, httpMethod: string, token?: string): Promise<Response>
{
    const headers = new Headers()
    headers.append("pragma", "no-cache")
    headers.append("cache-control", "no-cache")
    headers.append("Authorization", `Bearer ${ token }`)

    const requestInit = {
        method: httpMethod,
        headers: headers,
        body: body,
    }

    return fetchWithTimeout("http://localhost:8085/" + uri, requestInit, 5000)
}
